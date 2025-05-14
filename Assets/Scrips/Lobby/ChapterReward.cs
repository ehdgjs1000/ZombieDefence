using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterReward : MonoBehaviour
{
    public static ChapterReward instance;

    [SerializeField] private GameObject buyPromGO;
    [SerializeField] private GameObject[] buyPromImg;
    [SerializeField] private bool[] promotionType;


    private void Awake()
    {
        if(instance == null) instance = this;
    }
    private void Start()
    {
        PromotionUpdate();
        SyncPromServerToLocal();
    }
    public void BuyPromOnClick(int cost)
    {
        if(cost == 9900 && !promotionType[0])
        {
            buyPromGO.SetActive(true);
            BuyPromotion.instance.CostUpdate(cost);
            BackEndGameData.Instance.UserGameData.promotionType = 1;
        }else if (cost == 27000 && !promotionType[1])
        {
            buyPromGO.SetActive(true);
            BuyPromotion.instance.CostUpdate(cost);
            BackEndGameData.Instance.UserGameData.promotionType = 2;
        }
    }
    public void SyncPromServerToLocal()
    {
        if (BackEndGameData.Instance.UserGameData.promotionType == 1) promotionType[0] = true;
        else if (BackEndGameData.Instance.UserGameData.promotionType == 2) promotionType[1] = true;
    }
    public void PromotionUpdate()
    {
        if(BackEndGameData.Instance.UserGameData.promotionType == 1)
        {
            buyPromImg[0].SetActive(false);
        }else if (BackEndGameData.Instance.UserGameData.promotionType == 2)
        {
            buyPromImg[0].SetActive(false);
            buyPromImg[1].SetActive(false);
        }
    }

}
