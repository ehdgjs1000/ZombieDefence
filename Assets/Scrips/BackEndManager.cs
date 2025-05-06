using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndManager : MonoBehaviour
{

    private void Awake()
    {
        //Update() �޼ҵ��� Backend.AsyncPoll(); ȣ���� ���� ������Ʈ �ı�x
        DontDestroyOnLoad(gameObject);
        BackEndSetUp();
    }
    private void Update()
    {
        //������ �񵿱� �޼ҵ� ȣ��(�ݹ� �Լ� Ǯ��)�� ���� �ۼ�
        if (Backend.IsInitialized)
        {
            //Backend.AsyncPoll();
        }
    }
    private void BackEndSetUp()
    {
        //�ڳ� �ʱ�ȭ
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log($"�ʱ�ȭ ���� : {bro}");
        }
        else
        {
            Debug.LogError($"�ʱ�ȭ ���� : {bro}");
        }

    }


}
