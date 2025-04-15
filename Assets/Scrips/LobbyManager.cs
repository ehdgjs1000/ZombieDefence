using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] themas;

    //Setting Datas
    [SerializeField] private Army[] armyGos;
    [SerializeField] public Army[] chooseArmyGos;
    [SerializeField] private Image[] chooseArmyGosSprites;
    [SerializeField] private Sprite[] armySprites;
    private int chooseArmyCount = 0;

    //Main Datas


    private void Start()
    {
        SwapBtnOnlick(1);
    }
    public void StartBtnOnClick()
    {
        SceneManager.LoadScene(1);
        ChangeScene.instance.SetArmies(chooseArmyGos, chooseArmyCount);
    }
    public void ArmyBtnOnClick(int num)
    {
        ChooseArmy(num);
    }
    public void ChooseArmyBtnOnClick(int num)
    {
        chooseArmyGos[num] = null;
        chooseArmyGosSprites[num].sprite = null;
        chooseArmyCount--;
    }
    private void ChooseArmy(int _a)
    {
        int a = 0; 
        while (a <= chooseArmyGos.Length && chooseArmyCount<3)
        {
            if (chooseArmyGos[a] == null)
            {
                chooseArmyGos[a] = armyGos[_a];
                chooseArmyGosSprites[a].sprite = armySprites[_a];
                chooseArmyCount++;
                break;
            }else if (chooseArmyGos[a] != null)
            {
                a++;
            }
        }
    }
    public void SwapBtnOnlick(int num)
    {
        for (int a=0; a<themas.Length; a++)
        {
            if (a == num) themas[a].SetActive(true);
            else if(a != num) themas[a].SetActive(false);
        }
    }

}
