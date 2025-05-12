using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public bool isActive = false;
    public float questClearAmount = 0;

   
    private bool[] canClick = new bool[] {false, false, false, false, false, false, false};
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
    private IEnumerator CheckQuestClearAmount()
    {
        yield return new WaitForSeconds(0.05f);
        questClearAmount = BackEndGameData.Instance.UserQuestData.questClearAmount;
    }
    private void CheckQuestInfo()
    {
        if (questClearAmount >= 100) questClearAmount = 100;
        questRewardText.text = questClearAmount.ToString();
        
        if (AccountInfo.instance.isLogIn) canClick[0] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[0] >= questAmount[0]) canClick[1] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[1] >= questAmount[1]) canClick[2] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[2] >= questAmount[2]) canClick[3] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[3] >= questAmount[3]) canClick[4] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[4] >= questAmount[4]) canClick[5] = true;
        if (BackEndGameData.Instance.UserQuestData.questCount[5] >= questAmount[5]) canClick[6] = true;

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
        // 보상 적용
        if (canClick[num] && !BackEndGameData.Instance.UserQuestData.isReceived[num])
        {
            canClearImage[num].SetActive(true);
            questClearAmount += questReward[num];
            BackEndGameData.Instance.UserQuestData.questClearAmount = questClearAmount;
            BackEndGameData.Instance.UserQuestData.isReceived[num] = true;
            AccountInfo.instance.isClear[num] = true;
            Debug.Log("Lobbymanager");
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
