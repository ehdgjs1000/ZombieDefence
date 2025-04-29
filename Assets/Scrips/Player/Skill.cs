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
        float ranSkillChange = Random.Range(0.0f, 99.0f);
        if (ranSkillChange <= 88.0f)
        {
            ranSkill = Random.Range(0, 15);
            NormalSkillException(ranSkill);
        }
        else if (ranSkillChange > 88.0f && ranSkillChange <= 96.0f) //Special Skill
        {
            ranSkill = Random.Range(15,23);
            NormalSkillException(ranSkill);
        }
        else if (ranSkillChange > 96.0f && ranSkillChange <= 99.0f) //Epic SKill
        {
            ranSkill = Random.Range(23, 26);
            NormalSkillException(ranSkill);
        }

    }
    //�߰��Ǵ� Skill�� Gamemanager���� Skilldata �߰� ���ֱ�
    private void NormalSkillException(int ranNum)
    {
        // Skill Exception
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponNormal && skills[ranNum].skillLevel >=5)
        {
            SkillChoose();
        }
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponNormal && 
            !GameManager.instance.haveWeaponType[skills[ranNum].weaponType]) //�ش� ���Ⱑ �����Ǿ� �������
        {
            SkillChoose();
        }
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponSpecial && skills[ranNum].skillLevel >= 3)
        {
            SkillChoose();
        }
        if (skills[ranNum].skillType == SkillData.SkillType.UpgradeWeaponEpic && skills[ranNum].skillLevel >= 3)
        {
            SkillChoose();
        }

    }
    private void OnEnable()
    {
        Color color;
        textLevel.text = "Lv." + (skills[ranSkill].skillLevel + 1);
        Image skillBg = GetComponentInParent<Image>();
        switch (skills[ranSkill].skillType)
        {
            
            case SkillData.SkillType.UpgradeWeaponNormal:
                ColorUtility.TryParseHtmlString("#6F6F6F", out color);
                skillBg.color = color;
                textDesc.text = string.Format(skills[ranSkill].skillDesc);
                break;
            case SkillData.SkillType.UpgradeWeaponSpecial:
                ColorUtility.TryParseHtmlString("#148A00", out color);
                skillBg.color = color;
                textDesc.text = string.Format(skills[ranSkill].skillDesc);
                break;
            case SkillData.SkillType.UpgradeWeaponEpic:
                ColorUtility.TryParseHtmlString("#0E37C1", out color);
                skillBg.color = color;
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
            case (SkillData.SkillType.UpgradeWeaponSpecial):
                skills[ranSkill].skillLevel++;
                SkillManager.instance.UpgradeWeapon(skills[ranSkill].weaponType,
                    skills[ranSkill].upgradeType, skills[ranSkill].upgradeAmount);
                GameManager.instance.canUpgradeCheck = true;
                break;
            case (SkillData.SkillType.UpgradeWeaponEpic):
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
