using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    [SerializeField] private int rewardGold;
    [SerializeField] private int rewardCrystal;
    [SerializeField] private GameObject rewardItem;

    [SerializeField] private int rewardNum = 0;
    private bool isCleard = false;
    private bool canClick = false;
    private void Update()
    {
        CheckCanClick();
    }
    private void CheckCanClick()
    {
        if (QuestManager.instance.questClearAmount >= rewardNum)
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
            AccountInfo.instance.GetCash(0,rewardGold);
            AccountInfo.instance.GetCash(1,rewardCrystal);
        }
        
    }

}
