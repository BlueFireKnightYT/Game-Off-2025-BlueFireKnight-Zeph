using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Object.Destroy(gameObject);
        }
    }
}
