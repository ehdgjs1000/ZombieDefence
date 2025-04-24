using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private float[] upgradeConuts = new float[] { 2, 4, 8, 12, 18, 30, 60, 120 };
    [Header("Weapon Level")]
    [SerializeField] private Text[] pistolLevel;
    [SerializeField] private Text[] smgLevel;
    [SerializeField] private Text[] rifleLevel;
    [SerializeField] private Text[] srLevel;
    [SerializeField] private Text[] dmrLevel;

    [Header("Weapon Count")]
    [SerializeField] private Text[] pistolCount;
    [SerializeField] private Text[] smgCount;
    [SerializeField] private Text[] rifleCount;
    [SerializeField] private Text[] srCount;
    [SerializeField] private Text[] dmrCount;

    [Header("Weapon Progress bar")]
    [SerializeField] private Image[] pistolProgresses;
    [SerializeField] private Image[] smgProgresses;
    [SerializeField] private Image[] rifleProgresses;
    [SerializeField] private Image[] srProgresses;
    [SerializeField] private Image[] dmrProgresses;

    [Header("Weapon Bg")]
    [SerializeField] private GameObject[] pistolBg;
    [SerializeField] private Image[] smgBg;
    [SerializeField] private Image[] rifleBg;
    [SerializeField] private Image[] srBg;
    [SerializeField] private Image[] dmrBg;

    private void Update()
    {
        UpdateWeaponsProgress();
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
        }
        for (int sc = 0; sc < smgProgresses.Length; sc++)
        {
            smgProgresses[sc].fillAmount =
                (AccountInfo.instance.smgCount[sc] / upgradeConuts[AccountInfo.instance.smgLevel[sc]]);
            smgCount[sc].text = AccountInfo.instance.smgCount[sc].ToString();
            smgLevel[sc].text = "Lv." + AccountInfo.instance.smgLevel[sc].ToString();
        }
        for (int rc = 0; rc < rifleProgresses.Length; rc++)
        {
            rifleProgresses[rc].fillAmount =
                (AccountInfo.instance.rifleCount[rc] / upgradeConuts[AccountInfo.instance.rifleLevel[rc]]);
            rifleCount[rc].text = AccountInfo.instance.rifleCount[rc].ToString();
            rifleLevel[rc].text = "Lv." + AccountInfo.instance.rifleLevel[rc].ToString();
        }
        for (int sc = 0; sc < srProgresses.Length; sc++)
        {
            srProgresses[sc].fillAmount =
                (AccountInfo.instance.srCount[sc] / upgradeConuts[AccountInfo.instance.srLevel[sc]]);
            srCount[sc].text = AccountInfo.instance.srCount[sc].ToString();
            srLevel[sc].text = "Lv." + AccountInfo.instance.srLevel[sc].ToString();
        }
        for (int dc = 0; dc < dmrProgresses.Length; dc++)
        {
            dmrProgresses[dc].fillAmount =
                (AccountInfo.instance.dmrCount[dc] / upgradeConuts[AccountInfo.instance.dmrLevel[dc]]);
            dmrCount[dc].text = AccountInfo.instance.dmrCount[dc].ToString();
            dmrLevel[dc].text = "Lv." + AccountInfo.instance.dmrLevel[dc].ToString();
        }
    }

}
