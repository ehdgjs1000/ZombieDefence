using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;


public class RegisterAccount : LoginBase
{
    [SerializeField] private Image imageID;
    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private Image imagePW;
    [SerializeField] private TMP_InputField inputFieldPW;
    [SerializeField] private Image imageConfirmPW;
    [SerializeField] private TMP_InputField inputFieldConfirmPW;
    [SerializeField] private Image imageEmail;
    [SerializeField] private TMP_InputField inputFieldEmail;

    [SerializeField] private Button btnRegisterAccount;

    public void RegisterBtnOnClick()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail);

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "�н�����")) return;
        if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "�н����� Ȯ��")) return;
        if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "�����ּ�")) return;

        //��й�ȣ�� Ȯ���� ��ȣ�� �ٸ���
        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectEnterData(imageConfirmPW, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectEnterData(imageEmail, "���� ������ �߸��Ǿ����ϴ�. (ex. temp@xx.xx)");
            return;
        }
        btnRegisterAccount.interactable = false;
        SetMessage("���� �������Դϴ�...");

        CustomSignUp();
    }
    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text ,callback =>
        {
            btnRegisterAccount.interactable=true;

            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    SetMessage($"���� ���� ����. {inputFieldID.text}�� ȯ���մϴ�.");

                    //���� ������ �������� �� �ش� ������ ���� ���� ����
                    BackEndGameData.Instance.GameDataInsert();

                    Utils.LoadScene(SceneNames.LobbyScene);
                });
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409:
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403:
                    case 401:
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }
                if (message.Contains("���̵�"))
                {
                    GuideForIncorrectEnterData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }

        });
    }




}
