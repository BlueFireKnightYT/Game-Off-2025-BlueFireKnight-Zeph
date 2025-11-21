using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public GameManager Gm;
    public int SpeedForm;
    public int JumpForm;
    public int PotionForm;

    public void Start()
    {
            Gm = Object.FindFirstObjectByType<GameManager>();
    }
    public void Update()
    {       
        SpeedForm = Mathf.RoundToInt(4f + Mathf.Pow(2f, 0.5f * Gm.extraSpeed));
        JumpForm = Mathf.RoundToInt(4f + Mathf.Pow(2f, 0.5f * Gm.extraJumpHeight));
        PotionForm = Mathf.RoundToInt(9f + Mathf.Pow(2f, Gm.PotionFrequency));
    }
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
        if (GameManager.Instance.Coins > SpeedForm -1f)
        {
            GameManager.Instance.AddCoin(-SpeedForm);
            GameManager.Instance.AddSpeed(1);
        }
    }

    public void BuyHeight()
    {
        if (GameManager.Instance.Coins > JumpForm -1f)
        {
            GameManager.Instance.AddCoin(-JumpForm);
            GameManager.Instance.AddJumpHeight(1);
        }
    }

    public void BuyPotionFrequency()
    {
        if (GameManager.Instance.Coins > PotionForm -1f)
        {
            GameManager.Instance.AddCoin(-PotionForm);
            GameManager.Instance.AddPotionFrequency(1);
        }
    }

    public void resetGame()
    {
        PlayerPrefs.DeleteKey("PlayerCoins");
        PlayerPrefs.DeleteKey("PlayerExtraSpeed");
        PlayerPrefs.DeleteKey("PlayerExtraJumpHeight");
        PlayerPrefs.DeleteKey("PotionF");
    }
}
