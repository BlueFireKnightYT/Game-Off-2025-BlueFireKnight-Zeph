using UnityEditor;
using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    private bool Above300;
    private bool Above600;
    private Vector3 PiranhaPos;
    private bool StoppedArena;
    private bool StoppedSecondArena;
    private int PiranhaAmount;
    private int PiranhaSecondAmount;
    public bool InArena;
    public bool InSecondArena;

    public GameObject Surfboard;
    public GameObject Water;
    public Rigidbody2D WaterRB;
    public WaterRising wR;
    public SpawnPlatforms PS;
    public float LerpSpeed;
    public GameObject PiranhaPrefab;
    public int PiranhaMax;
    public int PiranhaSecondMax;
    public float ArenaTimer;
    public float SecondArenaTimer;
    public GameObject PiranhaGround;
    public GameObject PiranhaSecondGround;
    public GameObject LootUI;
    public SlotsController sC;
    public RainSpawn rS;
    void Update()
    {
        if (Above300 == false && transform.position.y > 298 && StoppedArena == false)
        {
            InArena = true;
            Above300 = true;
            wR.enabled = false;
            PS.enabled = false;
            PiranhaGround.GetComponent<BoxCollider2D>().enabled = true;
            Invoke("EndArena", ArenaTimer);
            InvokeRepeating("SpawnPiranhas", 0f, 0.5f);
        }
        if (Above300 && !StoppedArena)
        {
            Vector3 target = new Vector3(0, 294f, 0);

            Water.transform.position = Vector3.Lerp(
                Water.transform.position,
                target,
                LerpSpeed * Time.deltaTime
            );

            if (Water.transform.position.y >= 293.5f)
            {
                StoppedArena = true;
                Above300 = false;
                Debug.Log("miauw");
                WaterRB.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            foreach (GameObject obj in PS.spawnedPlatforms.ToArray())
            {
                if (obj == null) continue;

                if (obj.transform.position.y > 297)
                    Destroy(obj);
            }
        }

        if (Above600 == false && transform.position.y > 598 && StoppedSecondArena == false)
        {
            InSecondArena = true;
            Above600 = true;
            wR.enabled = false;
            PS.enabled = false;
            PiranhaSecondGround.GetComponent<BoxCollider2D>().enabled = true;
            Invoke("EndSecondArena", SecondArenaTimer);
            InvokeRepeating("SpawnPiranhasTwo", 0f, 0.5f);
            rS.InvokeRepeating("SpawnRain", 0f, 0.001f);
        }

        if (Above600 && !StoppedSecondArena)
        {
            Vector3 target = new Vector3(0, 594f, 0);

            Water.transform.position = Vector3.Lerp(
                Water.transform.position,
                target,
                LerpSpeed * Time.deltaTime
            );

            if (Water.transform.position.y >= 593.5f)
            {
                StoppedSecondArena = true;
                Above600 = false;
                WaterRB.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            foreach (GameObject obj in PS.spawnedPlatforms.ToArray())
            {
                if (obj == null) continue;

                if (obj.transform.position.y > 597)
                    Destroy(obj);
            }
        }

    }

    void SpawnPiranhas()
    { 
        PiranhaPos = new Vector3(Random.Range(-22, 22), Random.Range(294, 295), 0);
        Instantiate(PiranhaPrefab, PiranhaPos, Quaternion.identity);
        PiranhaAmount++;
        if (PiranhaAmount > PiranhaMax) CancelInvoke("SpawnPiranhas");
    }

    void SpawnPiranhasTwo()
    {
        PiranhaPos = new Vector3(Random.Range(-22, 22), Random.Range(594, 595), 0);
        Instantiate(PiranhaPrefab, PiranhaPos, Quaternion.identity);
        PiranhaSecondAmount++;
        if (PiranhaSecondAmount > PiranhaSecondMax) CancelInvoke("SpawnPiranhasTwo");
    }

    void EndArena()
    {
        InArena = false;
        wR.enabled = true;
        PS.enabled = true;
        Time.timeScale = 0f;
        LootUI.SetActive(true);
        sC.ClearAllSlots();
        Instantiate(Surfboard, transform.position, Quaternion.identity);
    }

    void EndSecondArena()
    {
        InSecondArena = false;
        wR.enabled = true;
        PS.enabled = true;
        rS.CancelInvoke("SpawnRain");
        Time.timeScale = 0f;
        LootUI.SetActive(true);
        sC.ClearAllSlots();
        Instantiate(Surfboard, transform.position, Quaternion.identity);
    }
}
