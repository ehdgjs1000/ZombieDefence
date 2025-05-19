using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponInfoSets;
    [SerializeField] private GameObject weaponTutorialPanel;

    public int weaponInfoNum = 0;
    private bool isSkipTutorial = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("IsSkipTutorial"))
        {
            PlayerPrefs.SetInt("IsSkipTutorial", 0);
            isSkipTutorial = false;
        }
        if (PlayerPrefs.GetInt("IsSkipTutorial") == 0) isSkipTutorial = false;
        else isSkipTutorial = true;

        if(!isSkipTutorial) weaponTutorialPanel.SetActive(true);
    }
    public void NextBtnOnClick()
    {
        if(weaponInfoNum < weaponInfoSets.Length-1)
        {
            weaponInfoNum++;
            for(int a = 0; a < weaponInfoSets.Length; a++)
            {
                if (a == weaponInfoNum) weaponInfoSets[a].SetActive(true);
                else  weaponInfoSets[a].SetActive(false);
            }
        }
    }
    public void BeforeBtnOnClick()
    {
        if (weaponInfoNum > 0)
        {
            weaponInfoNum--;
            for (int a = 0; a < weaponInfoSets.Length; a++)
            {
                if (a == weaponInfoNum) weaponInfoSets[a].SetActive(true);
                else weaponInfoSets[a].SetActive(false);
            }
        }
    }
    public void ExitBtnOnClick()
    {
        weaponTutorialPanel.SetActive(false);
        PlayerPrefs.SetInt("IsSkipTutorial",1);
    }

}
