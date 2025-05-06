using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private Loading loading;
    [SerializeField] private SceneNames nextScene;

    private bool canClick = false;


    private void Awake()
    {
        SystemSetup();
    }
    private void SystemSetup()
    {
        //Ȱ��ȭ ���� �ʾƵ� ����
        Application.runInBackground = true;
        
        //�ػ� ����
        int width = Screen.width;
        int height = (int)(Screen.width * 9/16.0f);
        Screen.SetResolution(width,height,true);

        //ȭ���� ������ �ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //�ε� ����
        loading.Play(OnAfterProgress);
    }
    private void OnAfterProgress()
    {
        canClick = true;
    }

    public void StartBtnOnClick()
    {
        if(canClick) Utils.LoadScene(nextScene);
    }
}
