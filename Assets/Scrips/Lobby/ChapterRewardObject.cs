using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterRewardObject : MonoBehaviour
{
    [SerializeField] private int GoldAmount;
    [SerializeField] private int CrystalAmount;
    [SerializeField] private GameObject cannotGetImg;
    [SerializeField] private bool[] rewardPromType;
    [SerializeField] private GameObject receivedImg;
    private bool canReward = false;
    private bool isRewared = false;

    private void Start()
    {
        if (canReward) cannotGetImg.SetActive(false);
    }
    private void Update()
    {
        PromotionUpdate();
    }
    public void PromotionUpdate()
    {
        int promotionType = BackEndGameData.Instance.UserGameData.promotionType;
        if (rewardPromType[0]) canReward = true;
        else if (rewardPromType[1] && (promotionType == 1 || promotionType == 2)) canReward = true;
        else if (rewardPromType[2] && promotionType == 2) canReward = true;
        if (canReward) cannotGetImg.SetActive(false);

        if(isRewared) receivedImg.SetActive(true);
        else receivedImg.SetActive(false);
    }
    public void ChapterRewardBtnOnClick()
    {
        if (!isRewared && canReward)
        {
            BackEndGameData.Instance.UserGameData.gold += GoldAmount;
            BackEndGameData.Instance.UserGameData.crystal += CrystalAmount;
            canReward = false;
            isRewared = true;
            BackEndGameData.Instance.GameDataUpdate();
            LobbyManager.instance.UpdateGameData();
        }
    }
}
