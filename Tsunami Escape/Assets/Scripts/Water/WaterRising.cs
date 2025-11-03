using UnityEngine;

public class WaterRising : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 1f);
    }
}
