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
        //활성화 되지 않아도 실행
        Application.runInBackground = true;
        
        //해상도 설정
        int width = Screen.width;
        int height = (int)(Screen.width * 9/16.0f);
        Screen.SetResolution(width,height,true);

        //화면이 꺼지지 않도록 유지
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //로딩 시작
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
