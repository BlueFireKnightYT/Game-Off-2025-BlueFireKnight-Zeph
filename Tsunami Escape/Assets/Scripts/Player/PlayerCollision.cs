using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputs pi;
    public float AntiGravity;
    public float NormalGravity;
    public WaterRising waterRising;
    public Countdown countdown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInputs>();
        rb.gravityScale = NormalGravity;
        if (waterRising == null) waterRising = Object.FindAnyObjectByType<WaterRising>();
        if (countdown == null) countdown = Object.FindAnyObjectByType<Countdown>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            SceneManager.LoadScene("DefeatScene");
        }
        if (other.CompareTag("Anti-Gravity Potion"))
        {
            rb.gravityScale = AntiGravity;
            Invoke("ApplyGrav", 10f);
            Destroy(other.gameObject);
            countdown.AntiGravTimer();

        }
        if (other.CompareTag("Slow-Time Potion"))
        { 
            waterRising.speed = waterRising.speed / 2f;
            Invoke("ResumeWater", 10f);
            Destroy(other.gameObject);

        }
    }

    private void ResumeWater()
    {
        waterRising.speed = waterRising.speed * 2;
    }

    private void ApplyGrav()
    { 
        if (pi.holdingDown == true)
        {
            rb.gravityScale = NormalGravity + 2;
        }
        else if(pi.holdingDown == false)
        {
            rb.gravityScale = NormalGravity;
        }
    }

}
