using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class FindID : LoginBase
{
    [SerializeField] private Image imageEmail;
    [SerializeField] private TMP_InputField inputFiedlEmail;

    [SerializeField] Button btnFinId;

    public void FindIDOnClick()
    {
        ResetUI(imageEmail);
        if (IsFieldDataEmpty(imageEmail, inputFiedlEmail.text, "메일 주소")) return;

        if (!inputFiedlEmail.text.Contains("@"))
        {
            GuideForIncorrectEnterData(imageEmail, "메일 형식이 잘못되었습니다. (temp@xx.xx)");
            return;
        }

        //"아이디 찾기" 상호작용 비활성화
        btnFinId.interactable = false;
        SetMessage("메일 발송중입니다...");

        FindCustomID();
    }
    private void FindCustomID()
    {
        // 아이디 정보를 이메일로 발송
        Backend.BMember.FindCustomID(inputFiedlEmail.text, callback =>
        {
            //아이디 찾기 버튼 활성화 
            btnFinId.interactable = true;

            if(callback.IsSuccess()){
                SetMessage($"{inputFiedlEmail.text} 주소로 메일을 발송했습니다.");
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:
                        message = "해당 메일을 사용하는 유저가 없습니다.";
                        break;
                    case 429:
                        message = "24시간 이내에 5회 이상 아이디/비밀번호 찾기를 시도했습니다.";
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
