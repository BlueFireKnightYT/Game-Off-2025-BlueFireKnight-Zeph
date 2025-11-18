using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformSpawning : MonoBehaviour
{
    [Header("References")]
    public Transform Player;
    public Camera mainCamera;
    public GameObject platformPrefab;
    public GameObject antiGravPrefab; // prefab for the anti-grav potion
    public GameObject slowMoPrefab;   // prefab for the slow-time potion
    public GameObject coinPrefab;     // prefab for a coin
    public Transform LevelAdvancer;
    [Header("Spawn Settings")]
    public float minYGap;
    public float maxYGap;
    public float minXGap;
    public float maxXGap;
    public int PlatformPerSpawn;
    public int widthLimit;
    public int heightLimit;
    public int Levelwidth;
    public float Buffer;

    private float lastY;
    private float lastX;
    private List<GameObject> platforms = new List<GameObject>();

    private void Start()
    {
        lastY = Player.position.y;
        lastX = Player.position.x;
    }
    private void Update()
    {
        float CameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize;
        if(CameraTop + Buffer > lastY)
        {
            SpawnPlatforms();
        }
    }

    private void SpawnPlatforms()
    {
        if (platformPrefab == null) return;

        // remove any destroyed entries
        platforms.RemoveAll(p => p == null);

        int maxSpawn = Mathf.Max(1, PlatformPerSpawn);
        int count = Random.Range(1, maxSpawn + 1);

        // get prefab size (fallback to (1,1) if none)
        Vector2 prefabSize = Vector2.one;
        var col2d = platformPrefab.GetComponent<Collider2D>();
        if (col2d != null) prefabSize = col2d.bounds.size;
        else
        {
            var rend = platformPrefab.GetComponent<Renderer>();
            if (rend != null) prefabSize = rend.bounds.size;
        }

        float halfW = prefabSize.x * 0.5f + Mathf.Max(minXGap, 0f);
        float halfH = prefabSize.y * 0.5f + Mathf.Max(minYGap, 0f);

        int maxAttemptsPerSpawn = 20;
        GameObject lastSpawned = null;

        // spawn platforms in this batch; bias X towards the sides so more platforms appear left/right
        float currentY = lastY;
        float currentX = lastX;

        // determine a starting Y for this batch (use first gap)
        float firstGapY = Random.Range(Mathf.Min(minYGap, maxYGap), Mathf.Max(minYGap, maxYGap));
        currentY += firstGapY;

        for (int i = 0; i < count; i++)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < maxAttemptsPerSpawn)
            {
                attempts++;

                // vertical: ensure minimum gap sequentially
                float yGap = Random.Range(Mathf.Min(minYGap, maxYGap), Mathf.Max(minYGap, maxYGap));
                float candidateY = currentY;
                if (i > 0) candidateY = currentY + yGap; // subsequent ones stack upward

                // horizontal: bias toward sides
                float sideBiasProbability = 0.75f; // increase to spawn more on sides
                float candidateX;
                float halfLevel = Levelwidth * 0.5f;
                if (Random.value < sideBiasProbability)
                {
                    // pick side (left or right) and choose a position near the edge
                    float edgeStart = Mathf.Lerp(halfLevel * 0.5f, Levelwidth, Random.value); // between 50% and 100% of half-level
                    float sign = (Random.value > 0.5f) ? 1f : -1f;
                    candidateX = sign * edgeStart;
                    // add a small jitter within min/max X gap range
                    float jitter = Random.Range(Mathf.Min(minXGap, maxXGap), Mathf.Max(minXGap, maxXGap)) * (Random.value > 0.5f ? 1f : -1f);
                    candidateX += jitter;
                }
                else
                {
                    // occasional center placements
                    candidateX = Random.Range(-halfLevel * 0.5f, halfLevel * 0.5f);
                }

                candidateX = Mathf.Clamp(candidateX, -Levelwidth, Levelwidth);

                // overlap check against existing platforms
                bool overlaps = false;
                foreach (var p in platforms)
                {
                    if (p == null) continue;
                    Vector3 pos = p.transform.position;
                    if (Mathf.Abs(candidateX - pos.x) < (halfW + halfW) && Mathf.Abs(candidateY - pos.y) < (halfH + halfH))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    GameObject newplat = Instantiate(platformPrefab, new Vector3(candidateX, candidateY, 0f), Quaternion.identity);
                    platforms.Add(newplat);
                    lastSpawned = newplat;
                    // update running positions for next iteration to maintain gaps
                    currentY = candidateY;
                    currentX = candidateX;
                    placed = true;

                    // 1-in-50 chance to spawn a potion on top of this platform
                    if (Random.Range(0, 50) == 0)
                    {
                        // pick potion type randomly: 50% anti-grav, 50% slow-time
                        GameObject chosenPotionPrefab = (Random.value < 0.5f) ? antiGravPrefab : slowMoPrefab;
                        string chosenTag = (chosenPotionPrefab == antiGravPrefab) ? "Anti-Gravity Potion" : "Slow-Time Potion";

                        if (chosenPotionPrefab != null)
                        {
                            // determine potion vertical offset from platform top
                            float platformHalfHeight = 0.5f;
                            var platCol = newplat.GetComponent<Collider2D>();
                            if (platCol != null) platformHalfHeight = platCol.bounds.extents.y;
                            else
                            {
                                var platR = newplat.GetComponent<Renderer>();
                                if (platR != null) platformHalfHeight = platR.bounds.extents.y;
                            }

                            // get potion prefab half height
                            float potionHalfHeight = 0.01f;
                            var potCol = chosenPotionPrefab.GetComponent<Collider2D>();
                            if (potCol != null) potionHalfHeight = potCol.bounds.extents.y;
                            else
                            {
                                var potR = chosenPotionPrefab.GetComponent<Renderer>();
                                if (potR != null) potionHalfHeight = potR.bounds.extents.y;
                            }

                            float potionOffset = platformHalfHeight + potionHalfHeight + 0.05f;
                            Vector3 potionPos = newplat.transform.position + Vector3.up * potionOffset;
                            GameObject potionInstance = Instantiate(chosenPotionPrefab, potionPos, Quaternion.identity);

                            // ensure the instantiated potion has the expected tag so PlayerCollision detects it
                            potionInstance.tag = chosenTag;
                        }
                    }

                    // 1-in-10 chance to spawn a coin on top of this platform
                    if (coinPrefab != null && Random.Range(0, 10) == 0)
                    {
                        // determine coin vertical offset from platform top
                        float platformHalfHeight = 0.5f;
                        var platCol2 = newplat.GetComponent<Collider2D>();
                        if (platCol2 != null) platformHalfHeight = platCol2.bounds.extents.y;
                        else
                        {
                            var platR2 = newplat.GetComponent<Renderer>();
                            if (platR2 != null) platformHalfHeight = platR2.bounds.extents.y;
                        }

                        // get coin prefab half height
                        float coinHalfHeight = 0.01f;
                        var coinCol = coinPrefab.GetComponent<Collider2D>();
                        if (coinCol != null) coinHalfHeight = coinCol.bounds.extents.y;
                        else
                        {
                            var coinR = coinPrefab.GetComponent<Renderer>();
                            if (coinR != null) coinHalfHeight = coinR.bounds.extents.y;
                        }

                        float coinOffset = platformHalfHeight + coinHalfHeight + 0.05f;
                        Vector3 coinPos = newplat.transform.position + Vector3.up * coinOffset;
                        GameObject coinInstance = Instantiate(coinPrefab, coinPos, Quaternion.identity);
                    }
                }
            }

            if (!placed)
            {
                Debug.LogWarning($"PlatformSpawning: skipped spawn {i} after {maxAttemptsPerSpawn} attempts to avoid overlap.");
                // still increment currentY so we don't try same Y forever
                currentY += Mathf.Max(minYGap, 0.5f);
            }
        }

        if (lastSpawned != null)
        {
            lastY = lastSpawned.transform.position.y;
            lastX = lastSpawned.transform.position.x;
        }
    }
}
