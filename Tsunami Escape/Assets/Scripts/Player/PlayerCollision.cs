using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("You Died");
            SceneManager.LoadScene("DefeatScene");
        }
    }
}
