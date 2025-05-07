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

        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "패스워드")) return;

        loginBtn.interactable = false;

        StartCoroutine(LoginProcess());

        //뒤끝 서버 로그인 시도
        ResponceToLogin(inputFieldID.text, inputFieldPW.text);
    }
    /// <summary>
    /// 로그인 시도 후 서버로부터 전달받을 message를 기반으로 처리
    /// </summary>
    private void ResponceToLogin(string ID, string PW)
    {
        Backend.BMember.CustomLogin(ID, PW, callback =>
          {
              StopCoroutine(nameof(LoginProcess));
              if (callback.IsSuccess())
              {
                  SetMessage($"{inputFieldID.text}님 환영합니다");

                  Utils.LoadScene(SceneNames.LobbyScene);
              }
              else //로그인 실패
              {
                  loginBtn.interactable = true;
                  string message = string.Empty;
                  switch (int.Parse(callback.GetStatusCode()))
                  {
                      case 401: //존재하지 않는 계정
                          message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디 입니다.": "잘못된 비밀번호 입니다.";
                          break;
                      case 403: //유저or디바이스 차단
                          message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다" :"차단당한 디바이스 입니다.";
                          break;
                      case 410: //탈퇴 진행중
                          message = "탈퇴가 진행중인 유저입니다.";
                          break;
                      default:
                          message = callback.GetMessage();
                          break;
                  }

                  if (message.Contains("비밀번호"))
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

            SetMessage($"로그인 중입니다...{time:F1}");
            yield return null;
        }
    }
}
