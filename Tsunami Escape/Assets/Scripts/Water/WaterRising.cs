using UnityEngine;

public class WaterRising : MonoBehaviour
{
    Animator animator;
    public float initialSpeed = 3f;  // starting speed
    public float acceleration = 1f;  // how much speed increases per second

    public float trueAcceleration;
    public float currentSpeed;
    public string[] tagsToIgnore = { "Player", "Platform" };
    private Collider2D myCollider;

    void Start()
    {
        trueAcceleration = acceleration * 0.05f;
        currentSpeed = initialSpeed;
        foreach (string tagName in tagsToIgnore)
        {
            GameObject[] objectsToIgnore = GameObject.FindGameObjectsWithTag(tagName);
            foreach (GameObject obj in objectsToIgnore)
            {
                Collider2D otherCollider = obj.GetComponent<Collider2D>();
                if (otherCollider != null)
                {
                    Physics2D.IgnoreCollision(myCollider, otherCollider);
                }
            }
        }
    }

    void Update()
    {
        // Increase speed based on acceleration
        currentSpeed += trueAcceleration * Time.deltaTime;

        // Move object upward
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    }
}
