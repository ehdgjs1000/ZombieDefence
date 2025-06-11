using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Stemina : MonoBehaviour
{
    [SerializeField] private Text energyText;
    [SerializeField] private TextMeshProUGUI timerText;
    private int maxEnergy = 30;
    private int currentEnergy;
    private int restoreDuration = 300;
    private DateTime nextEnergyTime;
    private DateTime lastEnergyTime;
    private bool isRestoring = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentEnergy"))
        {
            PlayerPrefs.SetInt("currentEnergy", 30);
            Load();
            StartCoroutine(RestoreEnergy());
        }
        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }
    public void UseEnergy()
    {
        if (currentEnergy >= -1)
        {
            currentEnergy--;
            UpdateEnergy();

            if (!isRestoring)
            {
                if (currentEnergy + 1 == maxEnergy)
                {
                    nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
                }
                StartCoroutine(RestoreEnergy());
            }

        }
        else
        {
            Debug.Log("No energy");
        }
    }
    private IEnumerator RestoreEnergy()
    {
        UpdateEnergyTimer(); 
        isRestoring = true;

        while (currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = nextEnergyTime;
            bool isEnergyAdding = false;

            while (currentDateTime > nextDateTime)
            {
                if (currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();
                    DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd,restoreDuration);
                }
                else
                {
                    break;
                }
            }

            if (isEnergyAdding == true)
            {
                lastEnergyTime = DateTime.Now;
                nextEnergyTime = nextDateTime;
            }

            UpdateEnergyTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }

        isRestoring = false;

    }
    private DateTime AddDuration(DateTime dateTime, int duration)
    {
        return dateTime.AddSeconds(duration);
    }
    private DateTime StringToDate(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime))
        {
            return DateTime.Now;
        }
        else
        {
            return DateTime.Parse(dateTime);
        }
    }
    private void UpdateEnergyTimer()
    {
        if (currentEnergy >= maxEnergy)
        {
            return;
        }
        TimeSpan time = nextEnergyTime - DateTime.Now;
        string timeValue = string.Format("{0:D2}:{1:D1}", time.Minutes, time.Seconds);
        timerText.text = timeValue;

    }
    private void UpdateEnergy()
    {
        //energyText.text = currentEnergy.ToString() + "/" + maxEnergy.ToString();
    }

    private void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));
    }
    private void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString());
    }


}
