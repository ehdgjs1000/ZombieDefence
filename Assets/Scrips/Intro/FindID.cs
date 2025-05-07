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
        if (IsFieldDataEmpty(imageEmail, inputFiedlEmail.text, "���� �ּ�")) return;

        if (!inputFiedlEmail.text.Contains("@"))
        {
            GuideForIncorrectEnterData(imageEmail, "���� ������ �߸��Ǿ����ϴ�. (temp@xx.xx)");
            return;
        }

        //"���̵� ã��" ��ȣ�ۿ� ��Ȱ��ȭ
        btnFinId.interactable = false;
        SetMessage("���� �߼����Դϴ�...");

        FindCustomID();
    }
    private void FindCustomID()
    {
        // ���̵� ������ �̸��Ϸ� �߼�
        Backend.BMember.FindCustomID(inputFiedlEmail.text, callback =>
        {
            //���̵� ã�� ��ư Ȱ��ȭ 
            btnFinId.interactable = true;

            if(callback.IsSuccess()){
                SetMessage($"{inputFiedlEmail.text} �ּҷ� ������ �߼��߽��ϴ�.");
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:
                        message = "�ش� ������ ����ϴ� ������ �����ϴ�.";
                        break;
                    case 429:
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))
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
