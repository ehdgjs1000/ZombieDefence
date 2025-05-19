using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWeapons : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Text levelTxt;
    

    public bool canClick = false;
    public bool canUpgrade = false;

    private void Update()
    {
        levelTxt.text = "Lv."+weaponData.weaponLevel.ToString();
    }
    public void UpgradeOnClick(int num)
    {
        if (canClick)
        {
            ShowUpgradePanel(num);
        }
    }
    public void CloseBtnOnClick()
    {
        CloseUpgradePanel();
    }
    private void ShowUpgradePanel(int num)
    {
        upgradePanel.SetActive(true);
        UpgradePanel.instance.SetPanelInfo(weaponData.weaponImage,Mathf.RoundToInt(weaponData.damage).ToString(),
            string.Format("{0:N3}",weaponData.fireRate) + "s",
            string.Format("{0:N3}", weaponData.reloadingTime) + "s",
            weaponData.maxBulletCount.ToString(), weaponData, weaponData.type,num);
        UpgradePanel.instance.CanUpgrade(canUpgrade);
    }
    private void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

}
