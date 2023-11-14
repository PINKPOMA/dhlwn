using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{

    public void INITUI(int score)
    {
        transform.Find("GameEndScore").GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
        Time.timeScale = 0;
    }

    public void GoToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
