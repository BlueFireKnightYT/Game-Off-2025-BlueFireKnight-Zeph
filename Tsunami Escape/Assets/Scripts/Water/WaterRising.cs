using UnityEngine;

public class WaterRising : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float speed = 3f;
    public PlayerCollision playerCollision;
    Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
    }
}
