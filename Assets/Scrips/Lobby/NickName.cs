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
        //�г��� ���濡 ������ ���� �޼��� ����� ����
        //�г��� ���� �˾��� �ٽ� �� �� �ְ� ���� �ʱ�ȭ
        ResetUI();
        SetMessage("�г����� �Է��ϼ���.");
    }
    public void UpdateNicknameOnClick()
    {
        ResetUI(imageNickname);

        if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

        updateNicknameBtn.interactable = false;
        SetMessage("�г��� �������Դϴ�...");

        //���� �г��� ���� �õ�
        UpdateNickname();
    }
    private void UpdateNickname()
    {
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            updateNicknameBtn.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}(��)�� �г����� ����Ǿ����ϴ�.");

                //�г��� ���濡 �����ϸ� onNickNameEvent�� ��ϵ� �̹�Ʈ ȣ��
                onNickNameEvent?.Invoke();
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message = "�г����� ����ְų� | 20�� �̻��̰ų� | ��/�ڿ� ������ �ֽ��ϴ�.";
                        break;
                    case 409:
                        message = "�̹� �����ϴ� �г����Դϴ�.";
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
