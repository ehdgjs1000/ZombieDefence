using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private UserGameData userGameData = new UserGameData();
    public UserGameData UserGameData => userGameData;

    [SerializeField] private GameObject initPanel;
    [SerializeField] private GameObject settingPanel;
     
    public void InitAccountBtnOnClick()
    {
        initPanel.SetActive(true);
    }
    public void InitPanelExitOnClick()
    {
        initPanel.SetActive(false);
    }
    public void InitOkayOnClick()
    {
        //�ʱ�ȭ ����
        Debug.Log("���� �ʱ�ȭ");
        PlayerPrefs.SetString("ID", null);
        PlayerPrefs.SetString("PW", null);
        PlayerPrefs.DeleteAll();

        BackEndGameData.Instance.GameDataReset();
        Save.instance.ResetWeaponJson();
        BackEndGameData.Instance.GameDataInsert();
        Utils.LoadScene("IntroScene");

    }
    public void ExitPanelOnClick()
    {
        settingPanel.SetActive(false);
    }


}
