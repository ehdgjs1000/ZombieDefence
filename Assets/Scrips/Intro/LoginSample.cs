using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class LoginSample : MonoBehaviour
{
    private void Awake()
    {
        string id = "userId";
        string pw = "1234";
        string email = "user@naver.com";
        string nickName = "ù����";

        //ȸ������
        Backend.BMember.CustomSignUp(id,pw);
        //�̸��� ����
        Backend.BMember.UpdateCustomEmail(email);
        //�α���
        Backend.BMember.CustomLogin(id,pw);
        //���̵� ã��
        Backend.BMember.FindCustomID(email);
        //��й�ȣ ã��
        Backend.BMember.ResetPassword(id,email);
        //�г��� ����
        //�г����� ������ ���� �г��� ����
        Backend.BMember.CreateNickname(nickName);
        
        Backend.BMember.UpdateNickname(nickName);
        
    
    }

}
