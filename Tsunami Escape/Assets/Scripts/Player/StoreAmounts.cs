using TMPro;
using UnityEngine;

public class StoreAmounts : MonoBehaviour
{
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI JumpHText;
    public TextMeshProUGUI PotionFText;

    void Start()
    {
        if (SpeedText == null) SpeedText = GetComponent<TextMeshProUGUI>();
        if (JumpHText == null) JumpHText = GetComponent<TextMeshProUGUI>();
        if (PotionFText == null) PotionFText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null || SpeedText == null) return;
        SpeedText.text = GameManager.Instance.extraSpeed.ToString();
        if (GameManager.Instance == null || JumpHText == null) return;
        JumpHText.text = GameManager.Instance.extraJumpHeight.ToString();
        if (GameManager.Instance == null || PotionFText == null) return;
        PotionFText.text = GameManager.Instance.PotionFrequency.ToString();

    }
}
