using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Army[] armyGos;
    [SerializeField] public Army[] chooseArmyGos;


    public void StartBtnOnClick()
    {
        SceneManager.LoadScene(1);
        ChangeScene.instance.SetArmies(armyGos);
    }
    public void ArmyBtnOnClick(int num)
    {
        switch (num)
        {
            case 0:
                ChooseArmy(num);
                break;
            case 1:
                ChooseArmy(num);
                break;
            case 2:
                ChooseArmy(num);
                break;
        }
    }
    private void ChooseArmy(int _a)
    {
        Debug.Log(chooseArmyGos.Length + " : " + armyGos.Length);
        int a = 0; 
        while (a <= chooseArmyGos.Length)
        {
            if (chooseArmyGos[a] == null)
            {
                chooseArmyGos[a] = armyGos[_a];
                break;
            }else if (chooseArmyGos[a] != null)
            {
                a++;
            }
        }
    }

}
