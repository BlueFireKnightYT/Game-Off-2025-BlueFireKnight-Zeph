using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float AntiGravtime;
    public Coroutine AntiGravTimer;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}
