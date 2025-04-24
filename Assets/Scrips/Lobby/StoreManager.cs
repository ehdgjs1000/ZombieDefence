using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject[] DrawGos; //#0 Draw1 #1 Draw10
    [SerializeField] private GameObject[] card10;
    [SerializeField] private GameObject card1;
    private CardFlip card1CF;
    CardFlip[] card10CF = new CardFlip[10];

    [SerializeField] private Sprite[] gunSprites;

    private void Awake()
    {
        card1CF = card1.GetComponent<CardFlip>();
        for (int a = 0; a < card10.Length; a++) card10CF[a] = card10[a].GetComponent<CardFlip>();
    }
    private void CardFlipReset()
    {
        for(int a = 0; a < card10.Length; a++) card10CF[a].ResetFlip();
        card1CF.ResetFlip();
    }
    public void DrawnBtnOnClick(int num) //»Ì±â ¹öÆ° Å¬¸¯ ÀÌº¥Æ®
    {
        if (num == 0) //ÇÑÀå »Ì±â
        {
            if(AccountInfo.instance.CashInfo(1) >= 100)
            {
                AccountInfo.instance.LoseCash(1, 100);
                DrawGos[0].SetActive(true);
                DrawGun(-1);
            }
            
        }else if (num == 1) //10Àå »Ì±â
        {
            if(AccountInfo.instance.CashInfo(1) >= 900)
            {
                AccountInfo.instance.LoseCash(1, 900);
                DrawGos[1].SetActive(true);
                for(int count = 0; count <10; count++) DrawGun(count);
            }
            
        }
    }
    public void DrawExitBtnOnClick()
    {
        CardFlipReset();
        for (int a = 0; a < DrawGos.Length; a++)
        {
            DrawGos[a].SetActive(false);
        }
        

    }
    public void DrawGun(int count)
    {
        //#normal 80% #Special 10% #Epic 6% #Hero 3% #Legendary 1%
        int ranVal = Random.Range(0,101);
        int gunSpriteNum = 0;
        int gunGrade = 0;
        if(ranVal <= 80) //normal
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
        }else if (ranVal > 80 && ranVal <=90) //Special
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
        }else if (ranVal >90 && ranVal<= 96) //Epic
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
        else if (ranVal > 96 && ranVal <=99) //Hero
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
        else //Legendary
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
        if (count == -1)
        {
            Image[] card1Imgs = card1.GetComponentsInChildren<Image>();
            card1Imgs[3].sprite = gunSprites[gunSpriteNum];
            CardBackColor(gunGrade,card1Imgs[2]);
            CardBackColor(gunGrade, card1Imgs[0]);
        }
        else
        {
            Image[] card10Imgs = card10[count].GetComponentsInChildren<Image>();
            card10Imgs[3].sprite = gunSprites[gunSpriteNum];
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
        }
    }
}
