using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    //#0 Pistol #1 SMG #2 Rifle #3 SR #4 Dmr
    public float[] damageRatio = { 100,100,100,100,100};
    public float[] tempFireRatio = { 100, 100, 100, 100, 100 };
    public float[] tempReloadration = { 100, 100, 100, 100, 100 };

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void UpgradeWeapon(int upgradeWeaponType,int upgradeType, float ratio)
    {
        if (upgradeType == 0) damageRatio[upgradeWeaponType] *= (100 + ratio)/100;
        else if (upgradeType == 1) tempFireRatio[upgradeWeaponType] *= (100 - ratio)/100;
        else if (upgradeType == 2) tempReloadration[upgradeWeaponType] *= (100 - ratio)/100;
    }
    public float[] GetWeaponData(int weaponType)
    {
        float[] data = new float[3];
        data[0] = damageRatio[weaponType];
        data[1] = tempFireRatio[weaponType];
        data[2] = tempReloadration[weaponType];
        return data;
    }


}
