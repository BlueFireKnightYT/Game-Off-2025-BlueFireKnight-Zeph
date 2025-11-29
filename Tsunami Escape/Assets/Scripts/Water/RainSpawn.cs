using UnityEngine;

public class RainSpawn : MonoBehaviour
{
    public float cooldown = 0.2f;
    private float _nextFireTime;
    public GameObject droplet;
    public GameObject player;
    public GameObject spawnedDroplet;
    public Rigidbody2D dropletRB;
    public void StartCooldowm()
    {
        _nextFireTime = Time.time + cooldown;
    }

    private void Start()
    {
        StartCooldowm();
    }

    public bool IsCoolingDown => Time.time < _nextFireTime;

    private void Update()
    {
        if(!IsCoolingDown)
        {
            spawnedDroplet = Instantiate(droplet, new Vector2(player.transform.position.x + Random.Range(-15, 15f), player.transform.position.y + 15), player.transform.rotation);
            StartCooldowm();
        }

        dropletRB = spawnedDroplet.GetComponent<Rigidbody2D>();
        if (dropletRB != null)
        {
            Destroy(spawnedDroplet, 5);
        }
    }
}


