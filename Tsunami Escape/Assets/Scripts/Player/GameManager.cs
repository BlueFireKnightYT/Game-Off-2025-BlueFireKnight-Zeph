using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // use int for coin counts
    public int Coins { get; private set; }

    [Header("Persistence")]
    [Tooltip("Save/load coins and player stats to PlayerPrefs when app quits / starts")]
    public bool usePlayerPrefs = true;
    private const string CoinsKey = "PlayerCoins";
    private const string SpeedKey = "PlayerExtraSpeed";
    private const string JumpKey = "PlayerExtraJumpHeight";

    [Header("Player Modifiers")]
    [Tooltip("Temporary or permanent extra speed applied to the player")]
    public float extraSpeed = 0f;

    [Tooltip("Temporary or permanent extra jump height applied to the player")]
    public float extraJumpHeight = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // load saved values
            if (usePlayerPrefs)
            {
                Coins = PlayerPrefs.GetInt(CoinsKey, 0);
                extraSpeed = PlayerPrefs.GetFloat(SpeedKey, extraSpeed);
                extraJumpHeight = PlayerPrefs.GetFloat(JumpKey, extraJumpHeight);
            }
            else
            {
                Coins = 0;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount = 1)
    {
        Coins += amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetInt(CoinsKey, Coins);
    }

    public void SetCoins(int amount)
    {
        Coins = amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetInt(CoinsKey, Coins);
    }

    // Speed helpers
    public void AddSpeed(float amount = 1f)
    {
        extraSpeed += amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(SpeedKey, extraSpeed);
    }

    public void SetSpeed(float amount)
    {
        extraSpeed = amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(SpeedKey, extraSpeed);
    }

    // Jump height helpers
    public void AddJumpHeight(float amount = 1f)
    {
        extraJumpHeight += amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(JumpKey, extraJumpHeight);
    }

    public void SetJumpHeight(float amount)
    {
        extraJumpHeight = amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(JumpKey, extraJumpHeight);
    }

    private void OnApplicationQuit()
    {
        if (usePlayerPrefs)
        {
            PlayerPrefs.SetInt(CoinsKey, Coins);
            PlayerPrefs.SetFloat(SpeedKey, extraSpeed);
            PlayerPrefs.SetFloat(JumpKey, extraJumpHeight);
        }
    }
}