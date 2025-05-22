using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshSkills : MonoBehaviour
{
    [SerializeField] private Skill[] skills;
    [SerializeField] private GameObject refreshBg;

    public void RefreshSkillOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        refreshBg.SetActive(true);
    }
    public void RefreshCrystalOnClick()
    {
        if (BackEndGameData.Instance.UserGameData.crystal >= 20)
        {
            BackEndGameData.Instance.UserGameData.crystal -= 20;
            SoundManager.instance.BtnClickPlay();
            foreach (Skill skill in skills)
            {
                skill.RefreshSkills();
            }
            BackEndGameData.Instance.GameDataUpdate();
            refreshBg.SetActive(false);
        }
        else
        {
            SoundManager.instance.ErrorClipPlay();
            refreshBg.SetActive(false);
        }

        
    }
    public void RefreshVideoOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        //비디오 시청후 리프레쉬
        GoogldAdmobs.instance.RefreshSkillsOnClick();
        foreach (Skill skill in skills)
        {
            skill.RefreshSkills();
        }
        refreshBg.SetActive(false);
    }

    public void RefreshExitBtnOnClick()
    {
        refreshBg.SetActive(false);
    }

}
