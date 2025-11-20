using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscoreTxt;
    public string sceneNameToCheck = "StoreScene";
    private void Start()
    {
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "StoreScene") return;
        else
        {
            highscoreTxt.SetText("Highscore: " + PlayerPrefs.GetInt("Highscore"));
        }
    }
}
