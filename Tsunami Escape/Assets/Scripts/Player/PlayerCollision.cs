using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputs pi;
    private Collider2D col;
    public float AntiGravity;
    public float NormalGravity;
    public WaterRising waterRising;
    public Countdown countdown;
    public Countdown2 countdown2;
    public Highscore hs;
    public DistanceBetween heightCalculator;

    public float checkDistance = 60f;
    public int rayCount = 3; // number of rays per side
    public float raySpacing = 0.5f; // vertical spacing between rays

    private Vector2 targetPosition;
    private bool shouldMove = false;
    private Vector2 zero;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInputs>();

        rb.gravityScale = NormalGravity;

        if (waterRising == null) waterRising = Object.FindAnyObjectByType<WaterRising>();
        if (countdown == null) countdown = Object.FindAnyObjectByType<Countdown>();
        if (countdown2 == null) countdown2 = Object.FindAnyObjectByType<Countdown2>();
    }

    private void Update()
    {
        if (shouldMove)
        {
            col.enabled = false;
            pi.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 30f * Time.deltaTime);
            if ((Vector2)transform.position == targetPosition)
            {
                shouldMove = false;
                Invoke("Ascend", 0f);
                Invoke("StopAscending", 2f);
                
            }          

            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance?.AddCoin(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Water"))
        {
            if (heightCalculator.distanceBetween > PlayerPrefs.GetInt("Highscore"))
            {

                PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(heightCalculator.distanceBetween));
            }
            SceneManager.LoadScene("DefeatScene");
            return;
        }

        if (other.CompareTag("Anti-Gravity Potion"))
        {
            rb.gravityScale = AntiGravity;
            Invoke("ApplyGrav", 10f);
            Destroy(other.gameObject);
            countdown?.AntiGravTimer();
            return;
        }

        if (other.CompareTag("Slow-Time Potion"))
        {
            if (waterRising != null)
            {
                waterRising.currentSpeed /= 2f;
                Invoke("ResumeWater", 10f);
            }
            Destroy(other.gameObject);
            countdown2?.SlowTimeTimer();
            return;
        }

        if (other.CompareTag("Surf"))
        {
            Destroy(other.gameObject);
            if(transform.position.x < 0) targetPosition = new Vector2(-28f, transform.position.y + 20);
            if(transform.position.x > 0) targetPosition = new Vector2(28f, transform.position.y + 20);
            zero = new Vector2(0f, 0f);
            rb.linearVelocity = zero;
            shouldMove = true;
        }
    }


    private void ResumeWater()
    {
        if (waterRising != null)
            waterRising.currentSpeed *= 2f;
    }

    private void ApplyGrav()
    {
        rb.gravityScale = (pi != null && pi.holdingDown) ? NormalGravity + 2 : NormalGravity;
    }

    private void Ascend()
    {
        rb.linearVelocityY = 20f;
    }
    private void StopAscending()
    {
        col.enabled = true;
        pi.enabled = true;
        rb.gravityScale = NormalGravity;
    }
}
