using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites; // assign in the Inspector (add as many as you need)

    void Start()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites == null || sprites.Length == 0)
            Debug.LogWarning("SpriteChanger: no sprites assigned in Inspector.");
    }

    void Update()
    {
        if (sprites == null || sprites.Length == 0) return;

        // Example thresholds for 3 sprites — add more conditions if you add more sprites.
        if (transform.position.y >= 200 && sprites.Length > 2)
            spriteRenderer.sprite = sprites[2];
        else if (transform.position.y >= 125 && sprites.Length > 1)
            spriteRenderer.sprite = sprites[1];
        else
            spriteRenderer.sprite = sprites[0];
    }
}
