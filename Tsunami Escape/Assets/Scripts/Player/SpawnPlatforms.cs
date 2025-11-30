using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform player;
    [SerializeField] Camera cam;
    [SerializeField] GameObject PlatformPrefab;
    [SerializeField] GameObject LowGravPrefab;
    [SerializeField] GameObject SlowTimePrefab;
    [SerializeField] GameObject SurfPrefab;
    [SerializeField] GameObject CoinPrefab;

    float minY;
    float maxY;
    int denomPotion;
    int denomSurf;
    [Header("Numbers Bullshit")]
    [SerializeField] float MaxYGap;
    [SerializeField] float AreaWidth;
    [SerializeField] int PlatformAmount;
    [SerializeField] float gapRadius = 5f;
    Vector2 ItemOffset;
    float LastY;
    public List<GameObject> spawnedPlatforms = new List<GameObject>();
    public List<GameObject> spawnedPlatformsTemp = new List<GameObject>();
    int Tries;
    void Start()
    {
        denomPotion = Mathf.RoundToInt(50f - 5f * GameManager.Instance.PotionFrequency);
        denomSurf = Mathf.RoundToInt(250f - 5f * GameManager.Instance.PotionFrequency);
        LastY = player.position.y;
    }

    void Update()
    {
        float CamTop = cam.transform.position.y + cam.orthographicSize;
        if(CamTop > LastY)
        {
            SpawnPlatform();
        }
        minY = LastY + 4;
        maxY = LastY + MaxYGap;
    }
    void SpawnPlatform()
    {
        for (int i  = 0; i < PlatformAmount; i++)
        {
            Vector2 spawnPos;
            Collider2D posCheck;

            Tries = 0;
            do
            {
              spawnPos = new Vector2(Random.Range(-AreaWidth, AreaWidth), Random.Range(minY, maxY));
              posCheck = Physics2D.OverlapCircle(spawnPos, gapRadius);
              Tries++;
             } while (posCheck != null && posCheck.CompareTag("Platform") && Tries < 25);
            GameObject LastPlatform = Instantiate(PlatformPrefab, spawnPos, Quaternion.identity);
            if (Random.Range(0, denomPotion) == 0)
            {
                GameObject chosenPotionPrefab = (Random.value < 0.5f) ? LowGravPrefab : SlowTimePrefab;
                ItemOffset = new Vector2(LastPlatform.transform.position.x, LastPlatform.transform.position.y + 0.5f);
                Instantiate(chosenPotionPrefab, ItemOffset, Quaternion.identity);
            }
            if(Random.Range(0, 20) == 0)
            {
                ItemOffset = new Vector2(LastPlatform.transform.position.x, LastPlatform.transform.position.y + 0.5f);
                Instantiate(CoinPrefab, ItemOffset, Quaternion.identity);
            }
            if (Random.Range(0, denomSurf) == 0)
            {
                ItemOffset = new Vector2(LastPlatform.transform.position.x, LastPlatform.transform.position.y + 0.5f);
                Instantiate(SurfPrefab, ItemOffset, Quaternion.identity);
            }
            spawnedPlatforms.Add(LastPlatform);
            spawnedPlatformsTemp.Add(LastPlatform);
        }
        GameObject lastSpawnedPlatform = spawnedPlatforms[spawnedPlatforms.Count - 1];
        spawnedPlatformsTemp.Clear();
        LastY = lastSpawnedPlatform.transform.position.y;
    }
}
