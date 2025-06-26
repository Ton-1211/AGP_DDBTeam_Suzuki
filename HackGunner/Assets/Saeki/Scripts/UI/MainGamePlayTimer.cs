using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainGamePlayTimer : MonoBehaviour
{
    private int mainGameSec, mainGameMin;
    [SerializeField] private TextMeshProUGUI TimerUI;
    
    // Start is called before the first frame update
    void Start()
    {
        mainGameSec = 0;
        mainGameMin = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeUpdateMainGame();
    }

    private void TimeUpdateMainGame()
    {     
        mainGameSec += 2;

        if (mainGameSec >= 6000)
        {
            mainGameSec = 0;
            mainGameMin++;
        }

        TimerUI.text = SetTimerText();
    }

    private string SetTimerText()
    {
        string TimeText = "";

        if (mainGameMin > 0)
        {
            TimeText = mainGameMin + ",";
        }

        TimeText += (mainGameSec / 100).ToString("00") + "," + (mainGameSec % 100).ToString("00");
        
        return TimeText;
    }
}
