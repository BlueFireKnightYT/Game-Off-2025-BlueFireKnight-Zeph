using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpH = 3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriterenderer;
    public GameObject pauseMenu;
    public bool holdingDown;
    private PlayerCollision playerCollision;
    private float horizontal;
    private void Start()
    {
        holdingDown = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocity.x != 0f)
        {
            animator.SetBool("isWalking", true);
            
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (rb.linearVelocity.y == 0)
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (rb.linearVelocity.x > 0)
        {
            spriterenderer.flipX = true;
        }
        else if (rb.linearVelocity.x < 0)
        {
            spriterenderer.flipX = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpH);
            animator.SetBool("isWalking", false);
            animator.SetBool("Jumping", true);
        }
    }

    public void MoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.gravityScale = rb.gravityScale + 2;
            holdingDown = true;
        }
        if (context.canceled)
        {
            holdingDown = false;
            rb.gravityScale = rb.gravityScale - 2;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.5f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed && pauseMenu.activeSelf)
        {
            
            pauseMenu.SetActive(false);

            Time.timeScale = 1f;
        }

        else if (context.performed && pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
