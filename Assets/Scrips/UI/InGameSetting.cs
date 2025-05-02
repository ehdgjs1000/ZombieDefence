using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingGo1;
    [SerializeField] private GameObject settingGo2;

    private void Start()
    {
        settingGo1.SetActive(true);
        settingGo2.SetActive(false);
    }
    public void SettingBtnOnClick()
    {
        settingGo2.SetActive(true);
        GameManager.instance.isStopGame = true;
    }
    public void SettingExitBtnOnClick()
    {
        this.gameObject.SetActive(false);
        settingGo1.SetActive(true);
        settingGo2.SetActive(false);
        Time.timeScale  = GameManager.instance.tempTimeScale;
    }
    public void SettinPanelExitBtnOnClick()
    {
        this.gameObject.SetActive(false);
        settingGo1.SetActive(true);
        settingGo2.SetActive(false);
        Time.timeScale = GameManager.instance.tempTimeScale;
    }
    public void ExitBtnOnClick()
    {
        SceneManager.LoadScene(0);
    }


}
