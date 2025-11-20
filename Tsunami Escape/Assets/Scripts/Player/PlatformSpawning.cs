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
    public GameObject antiGravPrefab; 
    public GameObject slowMoPrefab;   
    public GameObject coinPrefab;
    public GameObject surfboardPrefab;
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
    private int denom;
    private int denomSurf;

    private void Start()
    {
        denom = Mathf.RoundToInt(50f - 5f * GameManager.Instance.PotionFrequency);
        denomSurf = Mathf.RoundToInt(150f - 5f * GameManager.Instance.PotionFrequency);
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

        // Remove destroyed platforms from list
        platforms.RemoveAll(p => p == null);

        int maxSpawn = Mathf.Max(1, PlatformPerSpawn);
        int count = Random.Range(1, maxSpawn + 1);

        // Get platform prefab size
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

        float currentY = lastY;
        float currentX = lastX;

        // First platform vertical gap
        float firstGapY = Random.Range(Mathf.Min(minYGap, maxYGap), Mathf.Max(minYGap, maxYGap));
        currentY += firstGapY;

        for (int i = 0; i < count; i++)
        {
            bool placed = false;
            int attempts = 0;

            while (!placed && attempts < maxAttemptsPerSpawn)
            {
                attempts++;

                float yGap = Random.Range(Mathf.Min(minYGap, maxYGap), Mathf.Max(minYGap, maxYGap));
                float candidateY = (i == 0) ? currentY : currentY + yGap;

                // Horizontal placement (side bias)
                float sideBiasProbability = 0.75f;
                float candidateX;
                float halfLevel = Levelwidth * 0.5f;
                if (Random.value < sideBiasProbability)
                {
                    float edgeStart = Mathf.Lerp(halfLevel * 0.5f, Levelwidth, Random.value);
                    float sign = (Random.value > 0.5f) ? 1f : -1f;
                    candidateX = sign * edgeStart;

                    float jitter = Random.Range(Mathf.Min(minXGap, maxXGap), Mathf.Max(minXGap, maxXGap)) * (Random.value > 0.5f ? 1f : -1f);
                    candidateX += jitter;
                }
                else
                {
                    candidateX = Random.Range(-halfLevel * 0.5f, halfLevel * 0.5f);
                }

                candidateX = Mathf.Clamp(candidateX, -Levelwidth, Levelwidth);

                // Check for overlap with existing platforms
                bool overlaps = false;
                foreach (var p in platforms)
                {
                    if (p == null) continue;
                    Vector3 pos = p.transform.position;
                    if (Mathf.Abs(candidateX - pos.x) < (halfW * 2) && Mathf.Abs(candidateY - pos.y) < (halfH * 2))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps)
                {
                    GameObject newPlat = Instantiate(platformPrefab, new Vector3(candidateX, candidateY, 0f), Quaternion.identity);
                    platforms.Add(newPlat);
                    lastSpawned = newPlat;
                    currentY = candidateY;
                    currentX = candidateX;
                    placed = true;

                    // -----------------------
                    // Potion spawn
                    // -----------------------
                    if (Random.Range(0, denom) == 0 && (antiGravPrefab != null || slowMoPrefab != null))
                    {
                        GameObject chosenPotionPrefab = (Random.value < 0.5f) ? antiGravPrefab : slowMoPrefab;
                        if (chosenPotionPrefab != null)
                        {
                            float platHalfH = GetHalfHeight(newPlat);
                            float potionHalfH = GetHalfHeight(chosenPotionPrefab);
                            Vector3 potionPos = newPlat.transform.position + Vector3.up * (platHalfH + potionHalfH + 0.5f);
                            GameObject potion = Instantiate(chosenPotionPrefab, potionPos, Quaternion.identity);
                            potion.tag = (chosenPotionPrefab == antiGravPrefab) ? "Anti-Gravity Potion" : "Slow-Time Potion";
                        }
                    }

                    // -----------------------
                    // Coin spawn
                    // -----------------------
                    if (coinPrefab != null && Random.Range(0, 10) == 0)
                    {
                        float platHalfH = GetHalfHeight(newPlat);
                        float coinHalfH = GetHalfHeight(coinPrefab);
                        Vector3 coinPos = newPlat.transform.position + Vector3.up * (platHalfH + coinHalfH + 0.5f);
                        Instantiate(coinPrefab, coinPos, Quaternion.identity);
                    }

                    // -----------------------
                    // Surfboard spawn (1-in-100 chance)
                    // -----------------------
                    if (surfboardPrefab != null && Random.Range(1, denomSurf) == 1)
                    {
                        float platHalfH = GetHalfHeight(newPlat);
                        float surfHalfH = GetHalfHeight(surfboardPrefab);
                        Vector3 surfPos = newPlat.transform.position + Vector3.up * (platHalfH + surfHalfH + 0.5f);
                        Instantiate(surfboardPrefab, surfPos, Quaternion.identity);
                    }
                }
            }

            if (!placed)
            {
                Debug.LogWarning($"PlatformSpawning: skipped spawn {i} after {maxAttemptsPerSpawn} attempts.");
                currentY += Mathf.Max(minYGap, 0.5f);
            }
        }

        if (lastSpawned != null)
        {
            lastY = lastSpawned.transform.position.y;
            lastX = lastSpawned.transform.position.x;
        }
    }

    // -----------------------
    // Helper function to get prefab half-height
    // -----------------------
    private float GetHalfHeight(GameObject obj)
    {
        float halfH = 0.5f;
        var col = obj.GetComponent<Collider2D>();
        if (col != null) halfH = col.bounds.extents.y;
        else
        {
            var rend = obj.GetComponent<Renderer>();
            if (rend != null) halfH = rend.bounds.extents.y;
        }
        return halfH;
    }

}
