using UnityEditor;
using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    private bool Above300;
    private Vector3 PiranhaPos;
    private bool StoppedArena;
    private int PiranhaAmount;

    public GameObject Surfboard;
    public GameObject Water;
    public Rigidbody2D WaterRB;
    public WaterRising wR;
    public SpawnPlatforms PS;
    public float LerpSpeed;
    public GameObject PiranhaPrefab;
    public int PiranhaMax;
    public float ArenaTimer;
    void Update()
    {
        if (Above300 == false && transform.position.y > 298 && StoppedArena == false)
        {
            Above300 = true;
            wR.enabled = false;
            PS.enabled = false;
            Invoke("EndArena", ArenaTimer);
            InvokeRepeating("SpawnPiranhas", 0f, 0.5f);
            foreach (GameObject obj in PS.spawnedPlatforms)
            {
                if(obj.transform.position.y > 297)
                {
                    Destroy(obj);
                }
            }
        }
        if (Above300 && !StoppedArena)
        {
            Vector3 target = new Vector3(Water.transform.position.x, 294f, Water.transform.position.z);

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
        }

    }

    void SpawnPiranhas()
    { 
        PiranhaPos = new Vector3(Random.Range(-22, 22), Random.Range(294, 295), 0);
        Instantiate(PiranhaPrefab, PiranhaPos, Quaternion.identity);
        PiranhaAmount++;
        if (PiranhaAmount > PiranhaMax) CancelInvoke("SpawnPiranhas");
    }

    void EndArena()
    {
        wR.enabled = true;
        PS.enabled = true;
        Instantiate(Surfboard, transform.position, Quaternion.identity);
    }
}
