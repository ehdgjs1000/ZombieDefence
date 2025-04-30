using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public static UpgradePanel instance;

    private int[] upgradeCost = new int[] { 100, 200, 400, 1000, 2000, 5000, 13000, 30000 };
    private bool canUpgrade=false;
    [SerializeField] private GameObject upgradeInfoTxt;

    //weaponInfo
    public WeaponData weapon;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Text damageTxt;
    [SerializeField] private Text fireRateTxt;
    [SerializeField] private Text reloadTxt;
    [SerializeField] private Text maxMagTxt;
    [SerializeField] private Text upgradeCostText;
    private int weaponNum=0;
    private WeaponData.WeaponType weaponType;


    private void Awake()
    {
        if(instance == null) instance = this;
    }
    private void Update()
    {
        if (canUpgrade) upgradeInfoTxt.SetActive(true);
        else upgradeInfoTxt.SetActive(false);
    }
    public void SetPanelInfo(Sprite _weaponImage, string _damageTxt, string _fireRateTxt,
        string _reloadTxt, string _maxMagTxt, WeaponData _weaponData,
        WeaponData.WeaponType _weaponType, int _weaponNum)
    {
        weaponImage.sprite = _weaponImage;
        damageTxt.text = _damageTxt.ToString();
        fireRateTxt.text = _fireRateTxt.ToString();
        reloadTxt.text = _reloadTxt.ToString();
        maxMagTxt.text = _maxMagTxt.ToString();
        levelTxt.text = "LV."+_weaponData.weaponLevel.ToString();
        weapon = _weaponData;
        weaponNum = _weaponNum;
        weaponType = _weaponType;
        upgradeCostText.text = upgradeCost[_weaponData.weaponLevel].ToString();
        LobbyManager.instance.weaponNum = _weaponNum;
        LobbyManager.instance.weaponLevel = _weaponData.weaponLevel;
    }
    public void CanUpgrade(bool _canUpgrade)
    {
        canUpgrade = _canUpgrade;
    }

    //업그레이드
    public void UpgradeBtnOnClick()
    {
        if (canUpgrade && AccountInfo.instance.CashInfo(0) >= upgradeCost[weapon.weaponLevel])
        {
            AccountInfo.instance.LoseCash(0, upgradeCost[weapon.weaponLevel]);
            weapon.damage *= 1.1f;
            weapon.fireRate *= 0.9f;
            weapon.reloadingTime *= 0.9f;
            if(0<=weaponNum && weaponNum < 3)
            {
                AccountInfo.instance.pistolCount[weaponNum] -= Mathf.Pow(2, weapon.weaponLevel+1);
                weapon.weaponLevel++;
                AccountInfo.instance.pistolLevel[weaponNum]++;
            }else if(weaponNum >=3 && weaponNum < 5)
            {
                AccountInfo.instance.smgCount[weaponNum-3] -= Mathf.Pow(2, weapon.weaponLevel + 1);
                weapon.weaponLevel++;
                AccountInfo.instance.smgLevel[weaponNum-3]++;
            }
            else if (weaponNum >= 5 && weaponNum < 9)
            {
                AccountInfo.instance.rifleCount[weaponNum - 5] -= Mathf.Pow(2, weapon.weaponLevel + 1);
                weapon.weaponLevel++;
                AccountInfo.instance.rifleLevel[weaponNum - 5]++;
            }
            else if (weaponNum >= 9 && weaponNum < 11)
            {
                AccountInfo.instance.srCount[weaponNum - 9] -= Mathf.Pow(2, weapon.weaponLevel + 1);
                weapon.weaponLevel++;
                AccountInfo.instance.srLevel[weaponNum - 9]++;
            }
            else if (weaponNum >= 11 && weaponNum < 13)
            {
                AccountInfo.instance.dmrCount[weaponNum - 11] -= Mathf.Pow(2, weapon.weaponLevel+1);
                weapon.weaponLevel++;
                AccountInfo.instance.dmrLevel[weaponNum - 11]++;
            }
            else if (weaponNum >= 13 )
            {
                AccountInfo.instance.specialCount[weaponNum - 13] -= Mathf.Pow(2, weapon.weaponLevel + 1);
                weapon.weaponLevel++;
                AccountInfo.instance.specialLevel[weaponNum - 13]++;
            }
            canUpgrade = false;
            this.gameObject.SetActive(false);
        }
    }
}
