using TMPro;
using UnityEngine;

public class StoreAmounts : MonoBehaviour
{
    [Header("Upgrade Amount UI")]
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI JumpHText;
    public TextMeshProUGUI PotionFText;

    [Header("Upgrade Price UI")]
    public TextMeshProUGUI SpeedPrice;
    public TextMeshProUGUI JumpHPrice;
    public TextMeshProUGUI PotionPrice;

    private Mainmenu mainMenu;

    void Start()
    {
        mainMenu = FindFirstObjectByType<Mainmenu>();
    }

    void Update()
    {
        // Upgrade amounts
        SpeedText.text = GameManager.Instance.extraSpeed.ToString();
        JumpHText.text = GameManager.Instance.extraJumpHeight.ToString();
        PotionFText.text = GameManager.Instance.PotionFrequency.ToString();

        // Prices from Mainmenu
        SpeedPrice.text = mainMenu.SpeedForm.ToString();
        JumpHPrice.text = mainMenu.JumpForm.ToString();
        PotionPrice.text = mainMenu.PotionForm.ToString();
    }
}
