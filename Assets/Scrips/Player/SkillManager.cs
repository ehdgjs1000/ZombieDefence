using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    //#0 Pistol #1 SMG #2 Rifle #3 SR #4 Dmr
    public float[] damageRatio = { 100,100,100,100,100,100};
    public float[] tempFireRatio = { 100, 100, 100, 100, 100, 100};
    public float[] tempReloadration = { 100, 100, 100, 100, 100 ,100};
    public float[] canPanetrate = { 1,1,1 };

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    //#0 = Dagage #1 = FireRate #2 ReloadTime #3 Panetrate
    public void UpgradeWeapon(int upgradeWeaponType,int upgradeType, float ratio)
    {
        if (upgradeType == 0) damageRatio[upgradeWeaponType] *= (100 + ratio) / 100;
        else if (upgradeType == 1) tempFireRatio[upgradeWeaponType] *= (100 - ratio) / 100;
        else if (upgradeType == 2) tempReloadration[upgradeWeaponType] *= (100 - ratio) / 100;
        else if (upgradeType == 3) canPanetrate[upgradeWeaponType]++;
    }
    public float[] GetWeaponData(int weaponType)
    {
        float[] data = new float[4];
        data[0] = damageRatio[weaponType];
        data[1] = tempFireRatio[weaponType];
        data[2] = tempReloadration[weaponType];
        data[3] = canPanetrate[weaponType];
        
        return data;
    }


}
