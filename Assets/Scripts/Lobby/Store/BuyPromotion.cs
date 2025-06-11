using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPromotion : MonoBehaviour
{
    public static BuyPromotion instance;

    [SerializeField] private GameObject buyPromGO;
    [SerializeField] private TextMeshProUGUI costText;

    private int cost;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    public void CostUpdate(int _cost)
    {
        cost = _cost;
        costText.text = cost.ToString();
    }
    public void BuyOnClick()
    {
        //todo : 서버와 연동하여 과금하고 Promotion 뚫기
        if(cost == 9900)
        {
            BackEndGameData.Instance.UserGameData.promotionType = 1;
            ChapterReward.instance.PromotionUpdate();
            ExitBuyOnClick();
        }
        else if (cost == 27000)
        {
            BackEndGameData.Instance.UserGameData.promotionType = 2;
            ChapterReward.instance.PromotionUpdate();
            ExitBuyOnClick();
        }

    }
    public void ExitBuyOnClick()
    {
        buyPromGO.SetActive(false);
    }


}
