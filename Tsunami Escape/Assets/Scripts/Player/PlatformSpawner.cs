using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Prefabs")]
    public GameObject platformPrefab;

    [Header("Spawn Settings")]
    public float minYGap = 1.5f;
    public float maxYGap = 2.5f;
    public float minXGap = 1f;
    public float maxXGap = 2.5f;
    public int initialPlatforms = 10;
    public float spawnBuffer = 5f;
    public int maxPlatformsPerLevel = 3; // max platforms per "row"

    [Header("Level Bounds")]
    public float levelWidth = 3.5f;

    [Header("References")]
    public Transform player;
    public Camera mainCamera;

    private float lastY;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        lastY = player.position.y - 1f;

        for (int i = 0; i < initialPlatforms; i++)
            SpawnPlatformGroup();
    }

    void Update()
    {
        float cameraTop = mainCamera.transform.position.y + mainCamera.orthographicSize;

        // Spawn new platforms as player nears the top
        if (lastY < cameraTop + spawnBuffer)
            SpawnPlatformGroup();
    }

    void SpawnPlatformGroup()
    {
        // Determine the next group's vertical position
        float yGap = Random.Range(minYGap, maxYGap);
        float baseY = lastY + yGap;

        // How many platforms to spawn at this level
        int count = Random.Range(1, maxPlatformsPerLevel + 1);

        // Random starting horizontal position
        float startX = Random.Range(-levelWidth, levelWidth);

        for (int i = 0; i < count; i++)
        {
            float offsetX = i == 0 ? 0f : Random.Range(minXGap, maxXGap) * (Random.value < 0.5f ? -1f : 1f);
            float x = Mathf.Clamp(startX + offsetX, -levelWidth, levelWidth);

            // Small vertical variance
            float y = baseY + Random.Range(-0.3f, 0.3f);

            GameObject newPlat = Instantiate(platformPrefab, new Vector3(x, y, 0f), Quaternion.identity);
            platforms.Add(newPlat);
        }

        lastY = baseY;
    }
}
