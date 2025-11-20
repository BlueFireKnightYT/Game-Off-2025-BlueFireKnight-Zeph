using UnityEngine;

public class WaterRising : MonoBehaviour
{
    Animator animator;
    public float initialSpeed = 3f;  // starting speed
    public float acceleration = 1f;  // how much speed increases per second

    public float trueAcceleration;
    public float currentSpeed;

    void Start()
    {
        trueAcceleration = acceleration * 0.05f;
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        // Increase speed based on acceleration
        currentSpeed += trueAcceleration * Time.deltaTime;

        // Move object upward
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    }
}
