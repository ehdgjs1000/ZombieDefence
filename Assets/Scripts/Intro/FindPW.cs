using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class FindPW : LoginBase
{
    [SerializeField] private Image imageID;
    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private Image imageEmail;
    [SerializeField] private TMP_InputField inputFieldEmail;

    [SerializeField] private Button btnFindPW;
    [SerializeField] private AudioClip btnClip;

    public void FindPWOnClick()
    {
        ResetUI();

        SoundManager.instance.PlaySound(btnClip);
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldID.text, "메일주소")) return;

        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectEnterData(imageEmail, "메일 형식이 잘못되었습니다. (ex. temp@xx.xx)");
            return;
        }
        btnFindPW.interactable = false;
        SetMessage("메일 발송중입니다...");

        FindCustomPW();
    }
    private void FindCustomPW()
    {
        //비밀번호 초기화, 초기화된 비밀번호를 메일로 전송
        Backend.BMember.ResetPassword(inputFieldID.text, inputFieldEmail.text, callback =>
        {
            btnFindPW.interactable= true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} 주소로 메일을 발송하였습니다.");
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:
                        message = "해당 이메일을 사용하는 사용자가 없습니다.";
                        break;
                    case 429:
                        message = "24간 이내에 5회 이상 아이디/비밀번호 찾기를 시도했습니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("이메일"))
                {
                    GuideForIncorrectEnterData(imageEmail, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }

}
