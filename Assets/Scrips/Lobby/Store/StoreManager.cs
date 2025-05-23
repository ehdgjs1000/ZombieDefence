using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private BuyGoldGo buyGoldPanel;
    [SerializeField] private BuyGoldGo buyCrystalPanel;
    [SerializeField] private GameObject[] DrawGos; //#0 Draw1 #1 Draw10
    [SerializeField] private GameObject[] card10;
    [SerializeField] private GameObject card1;
    private CardFlip card1CF;
    CardFlip[] card10CF = new CardFlip[10];

    [SerializeField] private Sprite[] gunSprites;

    //Store GOs
    [SerializeField] private GameObject buyGoldGo;
    private int buyAmountType;
    private int buyCrystalType;
    [SerializeField] private GameObject buyCrystalGo;
    [SerializeField] private GameObject cardOkayBtn;
    [SerializeField] private GameObject cardExitBtn;

    private void Awake()
    {
        card1CF = card1.GetComponent<CardFlip>();
        for (int a = 0; a < card10.Length; a++) card10CF[a] = card10[a].GetComponent<CardFlip>();
    }
    private void CardFlipReset()
    {
        cardExitBtn.SetActive(false);
        cardOkayBtn.SetActive(true);
        for(int a = 0; a < card10.Length; a++) card10CF[a].ResetFlip();
        card1CF.ResetFlip();
    }
    public void ExitGoldOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        buyGoldGo.SetActive(false);
        buyCrystalGo.SetActive(false);
    }


    //골드 구매 초기창
    public void TryBuyGoldOnClick(int num)
    {
        SoundManager.instance.BtnClickPlay();
        buyGoldGo.SetActive(true);
        buyAmountType = num;

        if(num == 0) buyGoldPanel.UpdatePurchaseGoldInfo(100,1500);
        else if(num == 1) buyGoldPanel.UpdatePurchaseGoldInfo(600,10000);
        else buyGoldPanel.UpdatePurchaseGoldInfo(3000,50000);
    }
    public void TryBuyCrystalOnClick(int num)
    {
        SoundManager.instance.BtnClickPlay();
        buyCrystalGo.SetActive(true);
        buyCrystalType = num;

        if (num == 0) buyCrystalPanel.UpdatePurchaseCrystalInfo(3000, 900);
        else if (num == 1) buyCrystalPanel.UpdatePurchaseCrystalInfo(9900, 3500);
        else buyCrystalPanel.UpdatePurchaseCrystalInfo(49000, 13000);

    }
    private void UpdateServer()
    {
        BackEndGameData.Instance.GameDataUpdate();
        LobbyManager.instance.UpdateGameData();
    }
    public void BuyCrystalOnClick()
    {
        //현질 후 크리스탈 지급하기
        if (buyCrystalType == 0)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal += 900;
            UpdateServer();
        }
        else if (buyCrystalType == 1)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal += 3500;
            UpdateServer();
        }
        else if (buyCrystalType == 2)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal += 13000;
            UpdateServer();
        }
        else
        {
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("결제에 실패하였습니다.");
        }
        buyGoldGo.SetActive(false);
    }
    //골드 확실히 구매
    public void BuyGoldOnClick()
    {
        if(buyAmountType == 0 && BackEndGameData.Instance.UserGameData.crystal >= 100)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal -= 100;
            BackEndGameData.Instance.UserGameData.gold += 1500;
            BackEndGameData.Instance.UserQuestData.questCount[3] += 100;
            UpdateServer();
        }
        else if (buyAmountType == 1 && BackEndGameData.Instance.UserGameData.crystal >= 600)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal -= 600;
            BackEndGameData.Instance.UserGameData.gold += 10000;
            BackEndGameData.Instance.UserQuestData.questCount[3] += 600;
            UpdateServer();
        }
        else if (buyAmountType == 2 && BackEndGameData.Instance.UserGameData.crystal >= 3000)
        {
            SoundManager.instance.BtnClickPlay();
            BackEndGameData.Instance.UserGameData.crystal -= 3000;
            BackEndGameData.Instance.UserGameData.gold += 50000;
            BackEndGameData.Instance.UserQuestData.questCount[3] += 3000;
            UpdateServer();
        }
        else
        {
            SoundManager.instance.ErrorClipPlay();
            PopUpMessageBase.instance.SetMessage("다이아가 부족합니다.");
        }
        buyGoldGo.SetActive(false);
    }
    public void BuyGoldExitBtn()
    {
        SoundManager.instance.BtnClickPlay();
        buyGoldGo.SetActive(false);
    }

    public void DrawnBtnOnClick(int num) //뽑기 버튼 클릭 이벤트
    {
        if (num == 0) //한장 뽑기
        {
            if(BackEndGameData.Instance.UserGameData.crystal >= 100)
            {
                SoundManager.instance.BtnClickPlay();
                BackEndGameData.Instance.UserGameData.crystal -=100;
                DrawGos[0].SetActive(true);
                DrawGun(-1);
                BackEndGameData.Instance.UserQuestData.questCount[3] += 100;
                BackEndGameData.Instance.UserQuestData.questCount[0]++;
                UpdateServer();
            }
            else
            {
                SoundManager.instance.ErrorClipPlay();
                //다이아가 없을 경우
                PopUpMessageBase.instance.SetMessage("다이아가 부족합니다.");
            }
            
        }else if (num == 1) //10장 뽑기
        {
            if (BackEndGameData.Instance.UserGameData.crystal >= 900)
            {
                SoundManager.instance.BtnClickPlay();
                BackEndGameData.Instance.UserGameData.crystal -= 900;
                DrawGos[1].SetActive(true);
                for (int count = 0; count < 10; count++) DrawGun(count);
                BackEndGameData.Instance.UserQuestData.questCount[3] += 900;
                BackEndGameData.Instance.UserQuestData.questCount[0] += 10;
                UpdateServer();
            }
            else
            {
                SoundManager.instance.ErrorClipPlay();
                PopUpMessageBase.instance.SetMessage("다이아가 부족합니다.");
            }
        }
        LobbyManager.instance.SyncLobbyToAccount();

    }
    public void DrawOkayOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        StartCoroutine(DrawPlay());
    }
    IEnumerator DrawPlay()
    {
        cardOkayBtn.SetActive(false);
        StartCoroutine(FilpAll());
        yield return new WaitForSeconds(1.3f);
        cardExitBtn.SetActive(true);
    }
    public void DrawExitBtnOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        CardFlipReset();
        for (int a = 0; a < DrawGos.Length; a++)
        {
            DrawGos[a].SetActive(false);
        }
    }
    IEnumerator FilpAll()
    {
        for (int a = 0; a < card10.Length; a++)
        {
            card10CF[a].FlipBtnOnClick();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DrawGun(int count)
    {
        //#normal 85% #Special 10% #Epic 1% #Hero 3% #Legendary 0.15% #God 0.05%
        float ranVal = Random.Range(0.00f,100.00f);
        int gunSpriteNum = 0;
        int gunGrade = 0;
        if(ranVal <= 84.00f) //normal
        {
            gunGrade = 0;
            int ranN = Random.Range(0,2);
            switch (ranN)
            {
                case 0:
                    AccountInfo.instance.pistolCount[0]++;
                    gunSpriteNum = 0;
                    break;
                case 1:
                    AccountInfo.instance.rifleCount[0]++;
                    gunSpriteNum = 5;
                    break;
            }
        }else if (ranVal > 84.00f && ranVal <= 95.00f) //Special
        {
            gunGrade = 1;
            int ranS = Random.Range(0,2);
            switch (ranS)
            {
                case 0:
                    AccountInfo.instance.smgCount[0]++;
                    gunSpriteNum = 3;
                    break;
                case 1:
                    AccountInfo.instance.srCount[0]++;
                    gunSpriteNum = 9;
                    break;
            }
        }else if (ranVal > 95.00f && ranVal<= 98.00f) //Epic
        {
            gunGrade = 2;
            int ranE = Random.Range(0, 3);
            switch (ranE)
            {
                case 0:
                    AccountInfo.instance.pistolCount[1]++;
                    gunSpriteNum = 1;
                    break;
                case 1:
                    AccountInfo.instance.rifleCount[1]++;
                    gunSpriteNum = 6;
                    break;
                case 2:
                    AccountInfo.instance.dmrCount[0]++;
                    gunSpriteNum = 11;
                    break;
            }

        }
        else if (ranVal > 98.00f && ranVal <=99.50f) //Hero
        {
            gunGrade = 3;
            int ranH = Random.Range(0,4);
            switch (ranH)
            {
                case 0:
                    AccountInfo.instance.pistolCount[2]++;
                    gunSpriteNum = 2;
                    break;
                case 1:
                    AccountInfo.instance.smgCount[1]++;
                    gunSpriteNum = 4;
                    break;
                case 2:
                    AccountInfo.instance.rifleCount[2]++;
                    gunSpriteNum = 7;
                    break;
                case 3:
                    AccountInfo.instance.srCount[1]++;
                    gunSpriteNum = 10;
                    break;
            }
        }
        else if(ranVal > 99.50f && ranVal <= 99.89f) //Legendary
        {
            gunGrade = 4;
            int ranL = Random.Range(0,2);
            switch (ranL)
            {
                case 0:
                    AccountInfo.instance.rifleCount[3]++;
                    gunSpriteNum = 8;
                    break;
                case 1:
                    AccountInfo.instance.dmrCount[1]++;
                    gunSpriteNum = 12;
                    break;
            }
        }
        else //God
        {
            gunGrade = 5;
            AccountInfo.instance.specialCount[0]++;
            gunSpriteNum = 13;
        }

        if (count == -1)
        {
            Image[] card1Imgs = card1.GetComponentsInChildren<Image>();
            card1Imgs[1].sprite = gunSprites[gunSpriteNum];
            CardBackColor(gunGrade,card1Imgs[2]);
            CardBackColor(gunGrade, card1Imgs[0]);
        }
        else
        {
            Image[] card10Imgs = card10[count].GetComponentsInChildren<Image>();
            card10Imgs[1].sprite = gunSprites[gunSpriteNum];
            CardBackColor(gunGrade, card10Imgs[2]);
            CardBackColor(gunGrade, card10Imgs[0]);
        }
    }

    private void CardBackColor(int grade, Image card)
    {
        switch (grade)
        {
            case 0:
                card.color = Color.white;
                break;
            case 1:
                card.color = Color.green;
                break;
            case 2:
                card.color = Color.blue;
                break;
            case 3:
                Color color;
                ColorUtility.TryParseHtmlString("#F000FF", out color);
                card.color = color;
                break;
            case 4:
                card.color = Color.yellow;
                break;
            case 5:
                card.color = Color.red;
                break;
        }
    }
}
