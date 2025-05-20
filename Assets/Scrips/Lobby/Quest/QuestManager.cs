using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public bool isActive = false;
    public float questClearAmount = 0;

   
    public bool[] canClick = new bool[] {false, false, false, false, false, false, false};
    [SerializeField] private GameObject[] clearBGs;
    [SerializeField] private GameObject[] canClearImage;

    public float[] questCount = new float[6];
    private float[] questAmount = new float[] { 3,3,1000,100,2000,3};
    private int[] questReward = new int[] { 15,15,20,20,20,10,20};

    [SerializeField] private Image questProgressImg;
    [SerializeField] private Text questRewardText;
    [SerializeField] private TextMeshProUGUI[] questConds;
    [SerializeField] private Image[] questProgresses;
    [SerializeField] private TextMeshProUGUI[] questProText;
    [SerializeField] private QuestReward[] questRewards;

    //Date
    public DateTime nextResetTimeUTC; // 다음 초기화 시간
    private int beforeUtcDay;
    private int nowUtcDay;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        LoadUtcDay();
    }
    private void Start()
    {
        StartCoroutine(CheckQuestClearAmount());
    }
    private void Update()
    {
        if (LobbyManager.instance.themaNum == 1)
        {
            CheckQuestInfo();
            ClearBgCheck();
        }  
    }
    private void OnApplicationQuit()
    {
        SaveUtcDay();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadUtcDay();
        }
        else
        {
            SaveUtcDay();
        }
    }
    public void ResetQuest()
    {
        SaveUtcDay();
        //퀘스트 리셋
        //BackEndGameData.Instance.ResetQuestData();
        questClearAmount = 0;
        BackEndGameData.Instance.UserQuestData.Reset();
        for (int a = 0; a < 7; a++)
        {
            AccountInfo.instance.isClear[a] = false;
        }
        for (int a = 0; a < 5; a++)
        {
            questRewards[a].ResetQuest();
        }
        StartCoroutine(CheckQuestClearAmount());
    }
   
    private void SaveUtcDay()
    {
        PlayerPrefs.SetInt("UTCDay", DateTime.UtcNow.Day);
        PlayerPrefs.SetInt("UTCMonth", DateTime.UtcNow.Month);
        PlayerPrefs.SetInt("UTCYear", DateTime.UtcNow.Year);
    }
    public void ResetQUTC()
    {
        PlayerPrefs.SetInt("QUTCDay", DateTime.UtcNow.Day);
        PlayerPrefs.SetInt("QUTCMonth", DateTime.UtcNow.Month);
        PlayerPrefs.SetInt("QUTCYear", DateTime.UtcNow.Year);
    }
    private void LoadUtcDay()
    {
        beforeUtcDay = PlayerPrefs.GetInt("QUTCDay");
        Debug.Log("QUTCDay : " + beforeUtcDay);
        int beforeUtcMonth = PlayerPrefs.GetInt("QUTCMonth");
        int beforeUtcYear = PlayerPrefs.GetInt("QUTCYear");
        if (nowUtcDay > beforeUtcDay || DateTime.UtcNow.Month > beforeUtcMonth || DateTime.UtcNow.Year > beforeUtcYear)
        {
            ResetQuest();
            ResetQUTC();
        }
    }

    private IEnumerator CheckQuestClearAmount()
    {
        yield return new WaitForSeconds(0.05f);
        questClearAmount = BackEndGameData.Instance.UserQuestData.questClearAmount;
    }
    private void CheckQuestInfo()
    {
        if (questClearAmount >= 100) questClearAmount = 100;
        questRewardText.text = questClearAmount.ToString();
        
        canClick[0] = true;
        for (int a = 0; a < 6; a++)
        {
            if (BackEndGameData.Instance.UserQuestData.questCount[a] >= questAmount[a]) canClick[a+1] = true; else
            {
                canClick[a + 1] = false;
            }
        }

        questProgressImg.fillAmount = questClearAmount / 100;

        for (int a = 0; a<questConds.Length; a++)
        {
            if (a == 0) questConds[a].text = "1";
            else
            {
                questConds[a].text = questCount[a-1].ToString();
            }
        }
        for (int a = 0; a < questConds.Length; a++)
        {
            if (a == 0) questProgresses[a].fillAmount = 1;
            else
            {
                questProgresses[a].fillAmount = BackEndGameData.Instance.UserQuestData.questCount[a - 1] / questAmount[a - 1];
            }
        }
        for (int a= 0; a<questProText.Length; a++)
        {
            if (a == 0) questProText[a].text = "1";
            else
            {
                questProText[a].text = BackEndGameData.Instance.UserQuestData.questCount[a - 1].ToString();
            }
        }
    }
    public void QuestClearBtnOnClick(int num)
    {
        SoundManager.instance.BtnClickPlay();
        // 보상 적용
        if (canClick[num] && !BackEndGameData.Instance.UserQuestData.isReceived[num])
        {
            canClearImage[num].SetActive(true);
            questClearAmount += questReward[num];
            BackEndGameData.Instance.UserQuestData.questClearAmount = questClearAmount;
            BackEndGameData.Instance.UserQuestData.isReceived[num] = true;
            AccountInfo.instance.isClear[num] = true;
            BackEndGameData.Instance.GameDataUpdate();
        }
    }
    private void ClearBgCheck()
    {
        for (int a = 0; a < AccountInfo.instance.isClear.Length; a++)
        {
            if (canClick[a]) clearBGs[a].SetActive(true);
            else clearBGs[a].SetActive(false);
        }
        for (int a = 0; a< AccountInfo.instance.isClear.Length; a++)
        {
            if (BackEndGameData.Instance.UserQuestData.isReceived[a])
            {
                clearBGs[a].SetActive(true);
                canClearImage[a].SetActive(false);
            }
            else clearBGs[a].SetActive(false);
        }
    }



}
