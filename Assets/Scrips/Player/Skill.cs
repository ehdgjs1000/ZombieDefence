using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    public SkillData[] skills;
    public int level;

    private Image icon;
    private TextMeshProUGUI textLevel;
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textDesc;
    private int ranSkill;

    [SerializeField] private AudioClip upgradeClip;


    public void SkillUpdate()
    {
        SkillChoose();
        icon = GetComponentsInChildren<Image>()[2];
        icon.sprite = skills[ranSkill].skillIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
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
            SkillException(ranSkill);
        }
        else if (ranSkillChange > 88.0f && ranSkillChange <= 96.0f) //Special Skill
        {
            ranSkill = Random.Range(15,24);
            SkillException(ranSkill);
        }
        else if (ranSkillChange > 96.0f && ranSkillChange <= 99.0f) //Epic SKill
        {
            ranSkill = Random.Range(24, 29);
            SkillException(ranSkill);
        }

    }
    public void RefreshSkills()
    {
        Debug.Log("RefreshSkills");
        SkillUpdate();
        SkillInfoUpdate();
    }
    //�߰��Ǵ� Skill�� Gamemanager���� Skilldata �߰� ���ֱ�
    private void SkillException(int ranNum)
    {
        // Skill Exception
        if (!GameManager.instance.haveWeaponType[skills[ranNum].weaponType]) SkillChoose();

    }
    private void SkillInfoUpdate()
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
    private void OnEnable()
    {
        SkillInfoUpdate();
    }
    public void Onclick()
    {
        SoundManager.instance.PlaySound(upgradeClip);

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
