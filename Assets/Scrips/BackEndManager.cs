using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndManager : MonoBehaviour
{

    private void Awake()
    {
        //Update() 메소드의 Backend.AsyncPoll(); 호출을 위해 오브젝트 파괴x
        DontDestroyOnLoad(gameObject);
        BackEndSetUp();
    }
    private void Update()
    {
        //서버의 비동기 메소드 호출(콜백 함수 풀링)을 위해 작성
        if (Backend.IsInitialized)
        {
            //Backend.AsyncPoll();
        }
    }
    private void BackEndSetUp()
    {
        //뒤끝 초기화
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            Debug.LogError($"초기화 실패 : {bro}");
        }

    }


}
