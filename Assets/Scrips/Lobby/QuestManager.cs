using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public int questClearAmount = 0;

    public bool isLogIn = false;
    public int WeaponDraw = 0;
    public int playCount = 0;
    public int killZombieCount = 0;
    public int useCrystalCount = 0;
    public int useGoldCount = 0;
    public int upgradeCount = 0;
    private bool[] canClick = new bool[] {false, false, false, false, false, false, false};
    private bool[] isClear = new bool[] { false, false, false, false, false, false, false };
    [SerializeField] private GameObject[] clearBGs;
    [SerializeField] private GameObject[] canClearImage;
    private int[] questAmount = new int[] { 3,0,1000,100,2000,3};
    private int[] questReward = new int[] { 15,15,20,20,20,10,20};

    [SerializeField] private Text questRewardText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        CheckQuestInfo();
        ClearBgCheck();
    }
    private void CheckQuestInfo()
    {
        if (questClearAmount >= 100) questClearAmount = 100;
        questRewardText.text = questClearAmount.ToString();
        
        if (isLogIn) canClick[0] = true;
        if (WeaponDraw >= questAmount[0]) canClick[1] = true;
        if (playCount >= questAmount[1]) canClick[2] = true;
        if (killZombieCount >= questAmount[2]) canClick[3] = true;
        if (useCrystalCount>= questAmount[3]) canClick[4] = true;
        if (useGoldCount >= questAmount[4]) canClick[5] = true;
        if (upgradeCount >= questAmount[5]) canClick[6] = true;
    }
    public void QuestClearBtnOnClick(int num)
    {
        // 보상 적용
        if (canClick[num] && !isClear[num])
        {
            canClearImage[num].SetActive(true);
            questClearAmount += questReward[num];
            isClear[num] = true;
        }
    }
    private void ClearBgCheck()
    {
        for (int a = 0; a<isClear.Length; a++)
        {
            if (isClear[a])
            {
                clearBGs[a].SetActive(true);
                canClearImage[a].SetActive(false);
            }
            else clearBGs[a].SetActive(false);
        }
    }



}
