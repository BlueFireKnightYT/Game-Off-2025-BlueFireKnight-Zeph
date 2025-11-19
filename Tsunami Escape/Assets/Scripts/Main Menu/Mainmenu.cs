using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Store()
    {
        SceneManager.LoadScene("StoreScene");
    }

    public void BuySpeed()
    {
        if (GameManager.Instance.Coins > 4)
        {
            GameManager.Instance.AddCoin(-5);
            GameManager.Instance.AddSpeed(1);
        }
    }

    public void BuyHeight()
    {
        if (GameManager.Instance.Coins > 4)
        {
            GameManager.Instance.AddCoin(-5);
            GameManager.Instance.AddJumpHeight(1);
        }
    }

    public void BuyPotionFrequency()
    {
        if (GameManager.Instance.Coins > 9)
        {
            GameManager.Instance.AddCoin(-10);
            GameManager.Instance.AddPotionFrequency(1);
        }
    }
}
