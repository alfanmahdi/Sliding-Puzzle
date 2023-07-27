using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    public TextMeshProUGUI gameTimerText;

    public void SetTime(float gameTimer)
    {
        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;
        int hours = (int)(gameTimer / 3600) % 24;

        string timerString = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);

        gameTimerText.text = timerString;
    }
}
