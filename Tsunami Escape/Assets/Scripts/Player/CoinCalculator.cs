using TMPro;
using UnityEngine;

public class CoinCalculator : MonoBehaviour
{
    public TextMeshProUGUI coins;
    [SerializeField] private PlayerCollision pc; // assign in Inspector, or let Start() find it

    private void Start()
    {
        // auto-find if not set in Inspector
        if (pc == null) pc = FindObjectOfType<PlayerCollision>();
        if (coins == null) coins = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // defensive null checks
        if (pc == null || coins == null) return;

        // format as integer (change as needed)
        coins.text = Mathf.FloorToInt(pc.coins).ToString();
    }
}
