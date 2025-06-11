using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshSkills : MonoBehaviour
{
    [SerializeField] private Skill[] skills;
    [SerializeField] private GameObject refreshBg;

    private float btnClickCool = 2.0f;

    private void Update()
    {
        btnClickCool -= Time.deltaTime;
    }
    public void RefreshSkillOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        refreshBg.SetActive(true);
    }
    public void RefreshCrystalOnClick()
    {
        if (BackEndGameData.Instance.UserGameData.crystal >= 20 && btnClickCool <= 0.0f)
        {
            btnClickCool = 2.0f;
            BackEndGameData.Instance.UserGameData.crystal -= 20;
            SoundManager.instance.BtnClickPlay();
            foreach (Skill skill in skills)
            {
                skill.RefreshSkills();
            }
            BackEndGameData.Instance.GameDataUpdate();
            refreshBg.SetActive(false);
        }else if (btnClickCool > 0.0f)
        {
            PopUpMessageBase.instance.SetMessage("잠시 후 다시 클릭해주세요");
        }
        else
        {
            SoundManager.instance.ErrorClipPlay();
            refreshBg.SetActive(false);
        }

        
    }
    public void RefreshVideoOnClick()
    {
        if (btnClickCool <= 0.0f)
        {
            btnClickCool = 2.0f;
            SoundManager.instance.BtnClickPlay();
            //비디오 시청후 리프레쉬
            GoogldAdmobs.instance.RefreshSkillsOnClick();
            foreach (Skill skill in skills)
            {
                skill.RefreshSkills();
            }
            refreshBg.SetActive(false);
        }
        else PopUpMessageBase.instance.SetMessage("잠시 후 다시 클릭해주세요");
        
    }

    public void RefreshExitBtnOnClick()
    {
        refreshBg.SetActive(false);
    }

}
