using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Skill", menuName ="Scriptable Object/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType
    {
        UpgradeWeaponNormal,
        UpgradeWeaponSpecial,
        UpgradeWeaponEpic

    }

    [Header("#Weapon Skill Info")]
    public SkillType skillType;
    public int ID;
    public int weaponType;
    public int upgradeType; //#0 dmgup #1 firerateup #2 reloadup
    public float upgradeAmount;
    public int skillLevel;
    public string skillName;
    public Sprite skillIcon;

    [TextArea]
    public string skillDesc;


}
