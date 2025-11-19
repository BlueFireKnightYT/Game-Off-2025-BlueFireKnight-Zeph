using TMPro;
using UnityEngine;

public class CoinCalculator : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    void Start()
    {
        if (coinsText == null) coinsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.Instance == null || coinsText == null) return;
        coinsText.text = GameManager.Instance.Coins.ToString();
    }
}