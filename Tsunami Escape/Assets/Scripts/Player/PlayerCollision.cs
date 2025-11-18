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
    public Countdown2 countdown2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInputs>();
        rb.gravityScale = NormalGravity;
        if (waterRising == null) waterRising = Object.FindAnyObjectByType<WaterRising>();
        if (countdown == null) countdown = Object.FindAnyObjectByType<Countdown>();
        if (countdown2 == null) countdown2 = Object.FindAnyObjectByType<Countdown2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // increment central coin counter
            if (GameManager.Instance != null)
                GameManager.Instance.AddCoin(1);

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Water"))
        {
            SceneManager.LoadScene("DefeatScene");
            return;
        }

        if (other.CompareTag("Anti-Gravity Potion"))
        {
            rb.gravityScale = AntiGravity;
            Invoke("ApplyGrav", 10f);
            Destroy(other.gameObject);
            if (countdown != null) countdown.AntiGravTimer();
            return;
        }

        if (other.CompareTag("Slow-Time Potion"))
        {
            if (waterRising != null)
            {
                waterRising.speed = waterRising.speed / 2f;
                Invoke("ResumeWater", 10f);
            }
            Destroy(other.gameObject);
            if (countdown2 != null) countdown2.SlowTimeTimer();
            return;
        }
    }

    private void ResumeWater()
    {
        if (waterRising != null)
            waterRising.speed = waterRising.speed * 2;
    }

    private void ApplyGrav()
    {
        if (pi != null && pi.holdingDown)
        {
            rb.gravityScale = NormalGravity + 2;
        }
        else
        {
            rb.gravityScale = NormalGravity;
        }
    }
}
