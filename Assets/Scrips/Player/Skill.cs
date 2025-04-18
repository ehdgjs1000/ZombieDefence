using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData[] skills;
    public int level;

    private Image icon;
    private Text textLevel;
    private Text textName;
    private Text textDesc;
    private int ranSkill;


    public void SkillUpdate()
    {
        SkillChoose();
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = skills[ranSkill].skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = skills[ranSkill].skillName;
    }
    private void SkillChoose()
    {
        float ranSkillChange = Random.Range(0.0f, 80.0f);
        //Special Skill
        if (ranSkillChange <= 80.0f)
        {
            ranSkill = Random.Range(0, skills.Length);
            NormalSkillException(ranSkill);
        }
        else if (ranSkillChange > 80.0f && ranSkillChange <= 96.0f) //Normal Skill
        {

        }
        else //Epic Skill
        {

        }

    }
    //추가되는 Skill들 Gamemanager에서 Skilldata 추가 해주기
    private void NormalSkillException(int ranNum)
    {
        // Skill Exception
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponNormal && skills[ranNum].skillLevel >=5)
        {
            SkillChoose();
        }
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponNormal && 
            !GameManager.instance.haveWeaponType[skills[ranNum].weaponType]) //해당 무기가 장착되어 있을경우
        {
            SkillChoose();
        }
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponSpecial && skills[ranNum].skillLevel >= 3)
        {
            SkillChoose();
        }

    }
    private void OnEnable()
    {
        textLevel.text = "Lv." + (skills[ranSkill].skillLevel + 1);
        switch (skills[ranSkill].skillType)
        {
            case SkillData.SkillType.UpgradeWeaponNormal:
                textDesc.text = string.Format(skills[ranSkill].skillDesc);
                break;

        }
    }
    public void Onclick()
    {
        switch (skills[ranSkill].skillType)
        {
            case (SkillData.SkillType.UpgradeWeaponNormal):
                skills[ranSkill].skillLevel++;
                SkillManager.instance.UpgradeWeapon(skills[ranSkill].weaponType, 
                    skills[ranSkill].upgradeType, skills[ranSkill].upgradeAmount);
                GameManager.instance.canUpgradeCheck = true;
                break;

        }


        LevelUp.instance.HideLevelUp();
        GameManager.instance.isStopGame = false;
    }

}
