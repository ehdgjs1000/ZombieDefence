using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterRewardObject : MonoBehaviour
{
    [SerializeField] private int GoldAmount;
    [SerializeField] private int CrystalAmount;
    [SerializeField] private GameObject cannotGetImg;
    [SerializeField] private bool[] rewardPromType;
    [SerializeField] private GameObject receivedImg;
    [SerializeField] private int promOrder;
    public bool canReward = false;
    public bool isRewared = false;

    private void Start()
    {
        if (canReward) cannotGetImg.SetActive(false);
        PromotionUpdate();
    }
    private void Update()
    {
        if (rewardPromType[0])
        {
            canReward = ChapterReward.instance.canProm0Receive[promOrder];
            isRewared = ChapterReward.instance.isProm0Received[promOrder];
        }else if (rewardPromType[1])
        {

        }else if (rewardPromType[2])
        {

        }
        
        IsReceivedCheck();
    }
    public void PromotionUpdate()
    {
        int promotionType = BackEndGameData.Instance.UserGameData.promotionType;
        if (rewardPromType[0]) canReward = true;
        else if (rewardPromType[1] && (promotionType == 1 || promotionType == 2)) canReward = true;
        else if (rewardPromType[2] && promotionType == 2) canReward = true;
        if (canReward) cannotGetImg.SetActive(false);

        if (isRewared)
        {
            receivedImg.SetActive(true);
            Button btn = GetComponent<Button>();
            btn.enabled = false;
        }
        else receivedImg.SetActive(false);
        
    }
    public void IsReceivedCheck()
    {
        if (isRewared) receivedImg.SetActive(true);
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
            //수령했는지 체크
            ChapterReward.instance.isProm0Received[promOrder] = true;
            PromotionUpdate();
            receivedImg.SetActive(true);
            Button btn = GetComponent<Button>();
            btn.enabled = false;

            //Server전송
            BackEndGameData.Instance.GameDataUpdate();
            LobbyManager.instance.UpdateGameData();
        }
        Save.instance.SaveProm0Json();
    }
}
