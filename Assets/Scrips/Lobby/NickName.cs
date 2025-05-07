using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class NickName : LoginBase
{
    [System.Serializable]
    public class NicknameEvent : UnityEngine.Events.UnityEvent { }
    public NicknameEvent onNickNameEvent = new NicknameEvent();

    [SerializeField] private Image imageNickname;
    [SerializeField] private TMP_InputField inputFieldNickname;

    [SerializeField] private Button updateNicknameBtn;

    private void OnEnable()
    {
        //닉네임 변경에 실패해 에러 메세지 출력한 상태
        //닉네임 변경 팝업을 다시 열 수 있게 상태 초기화
        ResetUI();
        SetMessage("닉네임을 입력하세요.");
    }
    public void UpdateNicknameOnClick()
    {
        ResetUI(imageNickname);

        if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

        updateNicknameBtn.interactable = false;
        SetMessage("닉네임 변경중입니다...");

        //서버 닉네임 변경 시도
        UpdateNickname();
    }
    private void UpdateNickname()
    {
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            updateNicknameBtn.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}(으)로 닉네임이 변경되었습니다.");

                //닉네임 변경에 성공하면 onNickNameEvent에 등록된 이번트 호출
                onNickNameEvent?.Invoke();
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message = "닉네임이 비어있거나 | 20자 이상이거나 | 앞/뒤에 공백이 있습니다.";
                        break;
                    case 409:
                        message = "이미 존재하는 닉네임입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                GuideForIncorrectEnterData(imageNickname, message);
            }

            
        });
    }

}
