using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TimerTextTwo;
    public PlayerInputs pi;
    public ArenaHandler ah;
    private float RemainingTime;
    private float SecondRemainingTime;

    private void Start()
    {
        RemainingTime = ah.ArenaTimer;
        SecondRemainingTime = ah.SecondArenaTimer;
    }
    void Update()
    {
        if (ah.InArena)
        {
            RemainingTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(RemainingTime % 60);
            TimerText.enabled = true;
            TimerText.text = seconds.ToString();
        }
        else if (ah.InSecondArena)
        {
            SecondRemainingTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(SecondRemainingTime % 60);
            TimerTextTwo.enabled = true;
            TimerTextTwo.text = seconds.ToString();
        }
        else
        {
            TimerText.enabled = false;
            TimerTextTwo.enabled = false;
        }
            HealthText.text = pi.BaseHealth.ToString();
    }
}
