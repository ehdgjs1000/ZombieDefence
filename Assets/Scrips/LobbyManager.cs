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
            if (chooseArmyGos[0] == armyGos[_a]) break;
            else if (chooseArmyGos[1] == armyGos[_a]) break;
            else if (chooseArmyGos[2] == armyGos[_a]) break;
            if (chooseArmyGos[a] == null)
            {
                chooseArmyGos[a] = armyGos[_a];
                chooseArmyGosSprites[a].sprite = armySprites[_a];
                Color color;
                switch (armyGos[_a].GetComponent<Army>().weaponGrade)
                {
                    case 0:
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = Color.white;
                        ColorUtility.TryParseHtmlString("#B0B0B0", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                            
                        break;
                    case 1:
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = Color.green;
                        ColorUtility.TryParseHtmlString("#00AD07", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                        break;
                    case 2:
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = Color.blue;
                        ColorUtility.TryParseHtmlString("#4042FF", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                        break;
                    case 3:
                        ColorUtility.TryParseHtmlString("#F000FF", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = color;
                        ColorUtility.TryParseHtmlString("#A600A3", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                        break;
                    case 4:
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = Color.yellow;
                        ColorUtility.TryParseHtmlString("#C0B700", out color);
                        chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                        break;
                }
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
