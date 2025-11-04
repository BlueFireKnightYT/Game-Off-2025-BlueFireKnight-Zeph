using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputs pi;
    public float AntiGravity;
    public float NormalGravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInputs>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("You Died");
            SceneManager.LoadScene("DefeatScene");
        }
        if (other.CompareTag("Anti-Grav Potion"))
        {
            rb.gravityScale = AntiGravity;
            Invoke("ApplyGrav", 2f);
        }
    }

    private void ApplyGrav()
    { 
        if (pi.holdingDown == true)
        {
            Debug.Log("True");
            rb.gravityScale = NormalGravity + 2;
        }
        else if(pi.holdingDown == false)
        {
            Debug.Log("False");
            rb.gravityScale = NormalGravity;
        }
    }

}
