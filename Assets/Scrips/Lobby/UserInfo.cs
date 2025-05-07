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
            //���� �ҷ����� ����
            if (callback.IsSuccess())
            {
                //Json ������ �Ľ� ����
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
                //json �Ľ� ����
                catch (System.Exception e)
                {
                    data.Reset();
                    Debug.LogError(e);
                }
            }
            else //���� �ҷ����� ����
            {
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }

            //���� ���� �ҷ����� �Ϸ� �� onUserInfoEvent�� ��ϵ� �̺�Ʈ �޼ҵ� ȣ��
            onUserInfoEvent.Invoke();
        });
    }
}
public class UserInfoData
{
    public string gamerId;
    public string countryCode;
    public string nickName;             //�س���
    public string inDate;               //���� indate
    public string emailForFindPW;       //�̸��� �ּ�
    public string subscriptionType;     //Ŀ����, �䷯���̼� Ÿ��
    public string federationId;         //����, ���̽��� �� �䷯���̼� ID

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
