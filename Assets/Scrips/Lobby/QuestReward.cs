using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    [SerializeField] private int rewardGold;
    [SerializeField] private int rewardCrystal;
    [SerializeField] private GameObject rewardItem;
    [SerializeField] GameObject clearBg;

    [SerializeField] private int rewardNum = 0;
    private bool isCleard = false;
    private bool canClick = false;
    private void Update()
    {
        CheckCanClick();
    }
    private void CheckCanClick()
    {
        QuestClearCheck();
        if (QuestManager.instance.questClearAmount >= rewardNum)
        {
            canClick = true;
        }
    }
    public void RewardBtnOnClick()
    {
        if (canClick && !isCleard) 
        {
            //��������
            isCleard = true;
            BackEndGameData.Instance.UserGameData.gold += rewardGold;
            BackEndGameData.Instance.UserGameData.gold += rewardCrystal;
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
