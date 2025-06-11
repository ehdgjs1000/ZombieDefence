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
        string nickName = "첫유저";

        //회원가입
        Backend.BMember.CustomSignUp(id,pw);
        //이메일 설정
        Backend.BMember.UpdateCustomEmail(email);
        //로그인
        Backend.BMember.CustomLogin(id,pw);
        //아이디 찾기
        Backend.BMember.FindCustomID(email);
        //비밀번호 찾기
        Backend.BMember.ResetPassword(id,email);
        //닉네임 설정
        //닉네임이 없을때 최초 닉네임 설정
        Backend.BMember.CreateNickname(nickName);
        
        Backend.BMember.UpdateNickname(nickName);
        
    
    }

}
