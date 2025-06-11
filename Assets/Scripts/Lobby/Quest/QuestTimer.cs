using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class QuestTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;

    private DateTime nextUTCMidnight;
    private int nexyUTCY;
    private int nexyUTCM;
    private int nexyUTCD;

    private void Start()
    {
        //처음 UTC값 적용할떄
        if (!PlayerPrefs.HasKey("nextUTCY"))
        {
            nexyUTCY = DateTime.UtcNow.Year;
            nexyUTCM = DateTime.UtcNow.Month;
            nexyUTCD = DateTime.UtcNow.Day;

            PlayerPrefs.SetInt("nextUTCY", nexyUTCY);
            PlayerPrefs.SetInt("nextUTCM", nexyUTCM);
            PlayerPrefs.SetInt("nextUTCD", nexyUTCD);
        }
        else
        {
            nexyUTCY = PlayerPrefs.GetInt("nextUTCY");
            nexyUTCM = PlayerPrefs.GetInt("nextUTCM");
            nexyUTCD = PlayerPrefs.GetInt("nextUTCD");
        }
    }
    private void Update()
    {
        UpdateUTCTimer();
    }
    private void UpdateUTCTimer()
    {
        //날짜가 다를경우
        if(DateTime.UtcNow.Year != nexyUTCY || DateTime.UtcNow.Month != nexyUTCM || DateTime.UtcNow.Day != nexyUTCD)
        {
            PlayerPrefs.SetInt("nextUTCY", DateTime.UtcNow.Year);
            PlayerPrefs.SetInt("nextUTCM", DateTime.UtcNow.Month);
            PlayerPrefs.SetInt("nextUTCD", DateTime.UtcNow.Day);
            nexyUTCY = DateTime.UtcNow.Year;
            nexyUTCM = DateTime.UtcNow.Month;
            nexyUTCD = DateTime.UtcNow.Day;
        }

        TimeSpan ts = DateTime.UtcNow.TimeOfDay;
        timerTxt.text = ((new TimeSpan(24, 0, 0) - ts).Hours).ToString();
        timerTxt.text = (new TimeSpan(24, 0, 0) - ts).Hours + ":" + 
            (new TimeSpan(24, 0, 0) - ts).Minutes + ":" + (new TimeSpan(24, 0, 0) - ts).Seconds;
    }
}
