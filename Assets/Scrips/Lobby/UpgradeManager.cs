using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private float[] upgradeConuts = new float[] { 2, 4, 8, 16, 32, 64, 128, 256 };
    private int[] upgradeCost = new int[] { 100, 200, 400, 1000 , 2000, 5000, 13000, 30000};
    [Header("Weapon Level")]
    [SerializeField] private Text[] pistolLevel;
    [SerializeField] private Text[] smgLevel;
    [SerializeField] private Text[] rifleLevel;
    [SerializeField] private Text[] srLevel;
    [SerializeField] private Text[] dmrLevel;
    [SerializeField] private Text[] specialLevel;

    [Header("Weapon Count")]
    [SerializeField] private Text[] pistolCount;
    [SerializeField] private Text[] smgCount;
    [SerializeField] private Text[] rifleCount;
    [SerializeField] private Text[] srCount;
    [SerializeField] private Text[] dmrCount;
    [SerializeField] private Text[] specialCount;

    [Header("Weapon Progress bar")]
    [SerializeField] private Image[] pistolProgresses;
    [SerializeField] private Image[] smgProgresses;
    [SerializeField] private Image[] rifleProgresses;
    [SerializeField] private Image[] srProgresses;
    [SerializeField] private Image[] dmrProgresses;
    [SerializeField] private Image[] specialProgresses;

    [Header("Weapon Bg")]
    [SerializeField] private GameObject[] pistolBg;
    [SerializeField] private GameObject[] smgBg;
    [SerializeField] private GameObject[] rifleBg;
    [SerializeField] private GameObject[] srBg;
    [SerializeField] private GameObject[] dmrBg;
    [SerializeField] private GameObject[] specialBg;

    [SerializeField] private GameObject upgradePanel;

    private void Update()
    {
        UpdateWeaponsProgress();
    }
    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }
    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
    private void UpdateWeaponsProgress()
    {

        for (int pc = 0; pc < pistolProgresses.Length; pc++)
        {
            pistolProgresses[pc].fillAmount = 
                (AccountInfo.instance.pistolCount[pc]/upgradeConuts[AccountInfo.instance.pistolLevel[pc]]);
            pistolCount[pc].text = AccountInfo.instance.pistolCount[pc].ToString();
            pistolLevel[pc].text = "Lv." + AccountInfo.instance.pistolLevel[pc].ToString();
            if (AccountInfo.instance.pistolLevel[pc] > 0) pistolBg[pc].SetActive(false);
            if(AccountInfo.instance.pistolCount[pc] >= upgradeConuts[AccountInfo.instance.pistolLevel[pc]])
            {
                //업그레이드 가능
                pistolBg[pc].SetActive(false);
                pistolBg[pc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                pistolBg[pc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
            else
            {
                pistolBg[pc].GetComponentInParent<UpgradeWeapons>().canUpgrade = false;
            }
        }
        for (int sc = 0; sc < smgProgresses.Length; sc++)
        {
            smgProgresses[sc].fillAmount =
                (AccountInfo.instance.smgCount[sc] / upgradeConuts[AccountInfo.instance.smgLevel[sc]]);
            smgCount[sc].text = AccountInfo.instance.smgCount[sc].ToString();
            smgLevel[sc].text = "Lv." + AccountInfo.instance.smgLevel[sc].ToString();
            if (AccountInfo.instance.smgLevel[sc] > 0) smgBg[sc].SetActive(false);
            if (AccountInfo.instance.smgCount[sc] >= upgradeConuts[AccountInfo.instance.smgLevel[sc]])
            {
                //업그레이드 가능
                smgBg[sc].SetActive(false);
                smgBg[sc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                smgBg[sc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
        }
        for (int rc = 0; rc < rifleProgresses.Length; rc++)
        {
            rifleProgresses[rc].fillAmount =
                (AccountInfo.instance.rifleCount[rc] / upgradeConuts[AccountInfo.instance.rifleLevel[rc]]);
            rifleCount[rc].text = AccountInfo.instance.rifleCount[rc].ToString();
            rifleLevel[rc].text = "Lv." + AccountInfo.instance.rifleLevel[rc].ToString();
            if (AccountInfo.instance.rifleLevel[rc] > 0) rifleBg[rc].SetActive(false);
            if (AccountInfo.instance.rifleCount[rc] >= upgradeConuts[AccountInfo.instance.rifleLevel[rc]])
            {
                //업그레이드 가능
                rifleBg[rc].SetActive(false);
                rifleBg[rc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                rifleBg[rc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
        }
        for (int sc = 0; sc < srProgresses.Length; sc++)
        {
            srProgresses[sc].fillAmount =
                (AccountInfo.instance.srCount[sc] / upgradeConuts[AccountInfo.instance.srLevel[sc]]);
            srCount[sc].text = AccountInfo.instance.srCount[sc].ToString();
            srLevel[sc].text = "Lv." + AccountInfo.instance.srLevel[sc].ToString();
            if (AccountInfo.instance.srLevel[sc] > 0) srBg[sc].SetActive(false);
            if (AccountInfo.instance.srCount[sc] >= upgradeConuts[AccountInfo.instance.srLevel[sc]])
            {
                //업그레이드 가능
                srBg[sc].SetActive(false);
                srBg[sc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                srBg[sc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
        }
        for (int dc = 0; dc < dmrProgresses.Length; dc++)
        {
            dmrProgresses[dc].fillAmount =
                (AccountInfo.instance.dmrCount[dc] / upgradeConuts[AccountInfo.instance.dmrLevel[dc]]);
            dmrCount[dc].text = AccountInfo.instance.dmrCount[dc].ToString();
            dmrLevel[dc].text = "Lv." + AccountInfo.instance.dmrLevel[dc].ToString();
            if (AccountInfo.instance.dmrLevel[dc] > 0) dmrBg[dc].SetActive(false);
            if (AccountInfo.instance.dmrCount[dc] >= upgradeConuts[AccountInfo.instance.dmrLevel[dc]])
            {
                //업그레이드 가능
                dmrBg[dc].SetActive(false);
                dmrBg[dc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                dmrBg[dc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
        }
        for (int sc = 0; sc < specialProgresses.Length; sc++)
        {
            specialProgresses[sc].fillAmount =
                (AccountInfo.instance.specialCount[sc] / upgradeConuts[AccountInfo.instance.specialLevel[sc]]);
            specialCount[sc].text = AccountInfo.instance.specialCount[sc].ToString();
            specialLevel[sc].text = "Lv." + AccountInfo.instance.specialLevel[sc].ToString();
            if (AccountInfo.instance.specialLevel[sc] > 0) specialBg[sc].SetActive(false);
            if (AccountInfo.instance.specialCount[sc] >= upgradeConuts[AccountInfo.instance.specialLevel[sc]])
            {
                //업그레이드 가능
                specialBg[sc].SetActive(false);
                specialBg[sc].GetComponentInParent<UpgradeWeapons>().canClick = true;
                specialBg[sc].GetComponentInParent<UpgradeWeapons>().canUpgrade = true;
            }
        }
    }

}
