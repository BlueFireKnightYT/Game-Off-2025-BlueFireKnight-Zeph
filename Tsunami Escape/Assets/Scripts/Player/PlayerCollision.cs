using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputs pi;
    private Collider2D col;
    private Animator anim;
    private PlayerInput pI;
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
    public int BaseCoinValue;
    public float SurfDuration;
    public float SlowTimeAmount;
    public bool HasExtraLife;
    public bool AntiGrav;
    public RawImage Bite;
    public RawImage Hurt;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInputs>();
        pI = GetComponent<PlayerInput>();

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
            pI.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 30f * Time.deltaTime);
            if ((Vector2)transform.position == targetPosition)
            {
                shouldMove = false;
                Invoke("Ascend", 0f);
                Invoke("StopAscending", SurfDuration);
                
            }          

            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance?.AddCoin(BaseCoinValue);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Water"))
        {
            if(HasExtraLife == false)
            {
                if (heightCalculator.distanceBetween > PlayerPrefs.GetInt("Highscore"))
                {

                    PlayerPrefs.SetInt("Highscore", Mathf.RoundToInt(heightCalculator.distanceBetween));
                }
                SceneManager.LoadScene("DefeatScene");
            }
            else if (HasExtraLife)
            {
                rb.linearVelocityY = 50f;
                Invoke("Iframes", 1f);
            }
                return;
        }

        if (other.CompareTag("Anti-Gravity Potion"))
        {
            rb.gravityScale = AntiGravity;
            AntiGrav = true;
            Invoke("ApplyGrav", 10f);
            Destroy(other.gameObject);
            countdown?.AntiGravTimer();
            return;
        }

        if (other.CompareTag("Slow-Time Potion"))
        {
            if (waterRising != null)
            {
                waterRising.currentSpeed /= SlowTimeAmount;
                Invoke("ResumeWater", 10f);
            }
            Destroy(other.gameObject);
            countdown2?.SlowTimeTimer();
            return;
        }

        if (other.CompareTag("Surf"))
        {
            Destroy(other.gameObject);
            anim.SetBool("Surfing", true);
            if(transform.position.x < 0) targetPosition = new Vector2(-28f, transform.position.y + 20);
            if(transform.position.x > 0) targetPosition = new Vector2(28f, transform.position.y + 20);
            zero = new Vector2(0f, 0f);
            rb.linearVelocity = zero;
            shouldMove = true;
        }

        if (other.CompareTag("Piranha"))
        {
            pi.BaseHealth = pi.BaseHealth - 1;
            Color tempColor = Bite.color;
            tempColor.a = 0.2f;
            Bite.color = tempColor;
            Invoke("RemoveBite", 0.2f);
            Debug.Log(pi.BaseHealth);
        }

        if (other.CompareTag("droplet"))
        {
            pi.BaseHealth -= 1;
            Color tempColor = Hurt.color;
            tempColor.a = 0.2f;
            Hurt.color = tempColor;
            Invoke("RemoveHurt", 0.2f);
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
        pI.enabled = true;
        anim.SetBool("Surfing", false);
        rb.gravityScale = NormalGravity;
    }

    private void Iframes()
    {
        HasExtraLife = false;
    }

    private void RemoveBite()
    {
        Color tempColor = Bite.color;
        tempColor.a = 0f;
        Bite.color = tempColor;
    }

    private void RemoveHurt()
    {
        Color tempColor = Hurt.color;
        tempColor.a = 0f;
        Hurt.color = tempColor;
    }
}
