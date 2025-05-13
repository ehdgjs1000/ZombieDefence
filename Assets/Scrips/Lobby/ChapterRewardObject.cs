using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterRewardObject : MonoBehaviour
{
    [SerializeField] private int GoldAmount;
    [SerializeField] private int CrystalAmount;

    public void ChapterRewardBtnOnClick()
    {
        BackEndGameData.Instance.UserGameData.gold += GoldAmount;
        BackEndGameData.Instance.UserGameData.crystal += CrystalAmount;
        BackEndGameData.Instance.GameDataUpdate();
    }
}
