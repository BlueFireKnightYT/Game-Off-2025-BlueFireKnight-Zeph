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
    private const string PotionFKey = "PotionF";
    private const string HealthKey = "Health";

    [Header("Player Modifiers")]
    [Tooltip("Temporary or permanent extra speed applied to the player")]
    public float extraSpeed = 0f;

    [Tooltip("Temporary or permanent extra jump height applied to the player")]
    public float extraJumpHeight = 0f;

    [Tooltip("Frequency of how many potions spawn")]
    public float PotionFrequency = 0f;

    [Tooltip("Amount of Health of Player")]
    public float extraHealth = 5f;

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
                PotionFrequency = PlayerPrefs.GetFloat(PotionFKey, PotionFrequency);
                extraHealth = PlayerPrefs.GetFloat(HealthKey, extraHealth);
            }
            else
            {
                Coins = 0;
                extraSpeed = 0;
                extraJumpHeight = 0;   
                PotionFrequency = 0;
                extraHealth = 5;
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

    public void AddHealth(int amount = 1)
    {
        extraHealth += amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(HealthKey, extraHealth);
    }

    public void SetHealth(int amount)
    {
        extraHealth = amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(HealthKey, extraHealth);
    }

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

    public void AddPotionFrequency(float amount = 1f)
    {
        PotionFrequency += amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(PotionFKey, PotionFrequency);
    }

    public void SetPotionFrequency(float amount)
    {
        PotionFrequency = amount;
        if (usePlayerPrefs)
            PlayerPrefs.SetFloat(PotionFKey, PotionFrequency);
    }

    public void ResetSavedModifiers()
    {
        PlayerPrefs.DeleteKey(SpeedKey);
        PlayerPrefs.DeleteKey(JumpKey);
        PlayerPrefs.DeleteKey(PotionFKey);
        PlayerPrefs.DeleteKey(HealthKey);
        PlayerPrefs.Save();
        extraSpeed = 0f;
        extraJumpHeight = 0f;
        PotionFrequency = 0f;
        extraHealth = 5f;
        Debug.Log("GameManager: Modifiers reset.");
    }

    private void OnApplicationQuit()
    {
        if (usePlayerPrefs)
        {
            PlayerPrefs.SetInt(CoinsKey, Coins);
            PlayerPrefs.SetFloat(SpeedKey, extraSpeed);
            PlayerPrefs.SetFloat(JumpKey, extraJumpHeight);
            PlayerPrefs.SetFloat(PotionFKey, PotionFrequency);
            PlayerPrefs.SetFloat (HealthKey, extraHealth);
        }
    }
}