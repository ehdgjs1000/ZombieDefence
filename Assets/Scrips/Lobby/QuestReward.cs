using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    [SerializeField] private int rewardGold;
    [SerializeField] private int rewardCrystal;
    [SerializeField] private GameObject rewardItem;
    [SerializeField] GameObject clearBg;

    [SerializeField] private int rewardOrder;
    [SerializeField] private int rewardNum = 0;
    public bool isCleard = false;
    public bool canClick = false;
    private void Update()
    {
        CheckCanClick();
    }
    private void Start()
    {
        if(BackEndGameData.Instance.UserQuestData.isRewardReceived[rewardOrder]) isCleard = true;
    }
    private void CheckCanClick()
    {
        QuestClearCheck();
        if (QuestManager.instance.questClearAmount >= rewardNum && !isCleard)
        {
            canClick = true;
        }
    }
    public void RewardBtnOnClick()
    {
        if (canClick && !isCleard) 
        {
            //보상제공
            isCleard = true;
            BackEndGameData.Instance.UserGameData.gold += rewardGold;
            BackEndGameData.Instance.UserGameData.gold += rewardCrystal;
            BackEndGameData.Instance.UserQuestData.isRewardReceived[rewardOrder] = true;
            LobbyManager.instance.UpdateGameData();
            clearBg.SetActive(true);
        }
    }
    private void QuestClearCheck()
    {
        if(isCleard) clearBg.SetActive(true);
        else clearBg.SetActive(false);
    }

}
