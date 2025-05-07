using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class Login : LoginBase
{
    [SerializeField] private Image imageID;
    [SerializeField] private TMP_InputField inputFieldID;
    [SerializeField] private Image imagePW;
    [SerializeField] private TMP_InputField inputFieldPW;

    [SerializeField] Button loginBtn;

    public void LoginOnClick()
    {
        ResetUI();

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "�н�����")) return;

        loginBtn.interactable = false;

        StartCoroutine(LoginProcess());

        //�ڳ� ���� �α��� �õ�
        ResponceToLogin(inputFieldID.text, inputFieldPW.text);
    }
    /// <summary>
    /// �α��� �õ� �� �����κ��� ���޹��� message�� ������� ó��
    /// </summary>
    private void ResponceToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
          {
              StopCoroutine(nameof(LoginProcess));
              if (callback.IsSuccess())
              {
                  SetMessage($"{inputFieldID.text}�� ȯ���մϴ�");

                  Utils.LoadScene(SceneNames.LobbyScene);
              }
              else //�α��� ����
              {
                  loginBtn.interactable = true;
                  string message = string.Empty;
                  switch (int.Parse(callback.GetStatusCode()))
                  {
                      case 401: //�������� �ʴ� ����
                          message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵� �Դϴ�.": "�߸��� ��й�ȣ �Դϴ�.";
                          break;
                      case 403: //����or����̽� ����
                          message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�" :"���ܴ��� ����̽� �Դϴ�.";
                          break;
                      case 410: //Ż�� ������
                          message = "Ż�� �������� �����Դϴ�.";
                          break;
                      default:
                          message = callback.GetMessage();
                          break;
                  }

                  if (message.Contains("��й�ȣ"))
                  {
                      GuideForIncorrectEnterData(imagePW, message);
                  }else
                  {
                      GuideForIncorrectEnterData(imageID, message);
                  }
              }
          });
    }
    private IEnumerator LoginProcess()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;

            SetMessage($"�α��� ���Դϴ�...{time:F1}");
            yield return null;
        }
    }
}
