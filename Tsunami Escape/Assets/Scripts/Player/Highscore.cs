using System;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscoreTxt;
    private void Start()
    {
    }
    private void Update()
    {
        highscoreTxt.SetText("Highscore: " + PlayerPrefs.GetInt("Highscore"));
    }
}
