using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class UserInfo : MonoBehaviour
{
    [System.Serializable] 
    public class UserInfoEvent : UnityEngine.Events.UnityEvent { }
    public UserInfoEvent onUserInfoEvent = new UserInfoEvent();

    private static UserInfoData data = new UserInfoData();
    public static UserInfoData Data => data;


    public void GetUserInfoFromBackEnd()
    {
        Backend.BMember.GetUserInfo(callback =>
        {
            //정보 불러오기 성공
            if (callback.IsSuccess())
            {
                //Json 데이터 파싱 성공
                try
                {
                    Debug.Log("success");
                    JsonData json = callback.GetReturnValuetoJSON()["row"];
                    
                    data.gamerId = json["gamerId"].ToString();
                    data.countryCode = json["countryCode"]?.ToString();
                    data.nickName = json["nickname"]?.ToString();
                    data.inDate = json["inDate"].ToString();
                    data.emailForFindPW = json["emailForFindPassword"]?.ToString();
                    data.subscriptionType = json["subscriptionType"].ToString();
                    //data.federationId = json["federationId"].ToString();
                }
                //json 파싱 실패
                catch (System.Exception e)
                {
                    data.Reset();
                    Debug.LogError(e);
                }
            }
            else //정보 불러오기 실패
            {
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }

            //유저 정보 불러오기 완료 후 onUserInfoEvent에 등록된 이벤트 메소드 호출
            onUserInfoEvent.Invoke();
        });
    }
}
public class UserInfoData
{
    public string gamerId;
    public string countryCode;
    public string nickName;             //넥네임
    public string inDate;               //유저 indate
    public string emailForFindPW;       //이메일 주소
    public string subscriptionType;     //커스텀, 페러데이션 타입
    public string federationId;         //구글, 페이스북 등 페러데이션 ID

    public void Reset()
    {
        gamerId = "OffLine";
        countryCode = "Unknown";
        nickName = "Unknown";
        inDate = string.Empty;
        emailForFindPW = string.Empty;
        subscriptionType = string.Empty;
        federationId = string.Empty;
    }
}
