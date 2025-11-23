using Unity.VisualScripting;
using UnityEngine;

public class PiranhaMovement : MonoBehaviour
{
    public float MaxLeft;
    public float MaxRight;
    public float Speed = 5f;
    private Collider2D myCollider;
    public string[] tagsToIgnore = { "Player", "Platform" };

    private Rigidbody2D rb;
    private int direction = 1;
    private float LeftMax;
    private float RightMax;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(JumpingPiranha), 0f, 5f);

        float startX = transform.position.x;
        LeftMax = startX + MaxLeft;
        RightMax = startX + MaxRight; 

        myCollider = GetComponent<Collider2D>();

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

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * Speed, rb.linearVelocityY);
        if (rb.position.x >= RightMax)
            direction = -1;
        else if (rb.position.x <= LeftMax)
            direction = 1;
        if (direction == 1)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    void JumpingPiranha()
    {
        if(Random.Range(0, 2) == 0)
        {
            rb.linearVelocityY = 15f;
        }
    }
}
