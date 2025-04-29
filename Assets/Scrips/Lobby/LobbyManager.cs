using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{  
    public static LobbyManager instance;

    public GameObject[] themas;

    //Setting Datas
    [SerializeField] private Army[] armyGos;
    [SerializeField] public Army[] chooseArmyGos;
    [SerializeField] private Image[] chooseArmyGosSprites;
    [SerializeField] private Sprite[] armySprites;
    private int chooseArmyCount = 0;

    //WeaponDatas
    [SerializeField] private WeaponData[] weaponDatas;
    private WeaponData[] tempWeaponDatas = new WeaponData[13];
    public int weaponNum=0;
    public int weaponLevel;

    //AccountInfo
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text CrystalTxt;

    //Main Datas
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        for(int a = 0; a < weaponDatas.Length; a++)
        {
            tempWeaponDatas[a] = weaponDatas[a];
        }
    }
    private void OnApplicationQuit()
    {
        for (int a = 0; a < weaponDatas.Length; a++)
        {
            weaponDatas[a] = tempWeaponDatas[a];
        }
    }

    private void Start()
    {
        SwapBtnOnlick(1);
    }
    private void Update()
    {
        goldTxt.text = AccountInfo.instance.CashInfo(0).ToString();
        CrystalTxt.text = AccountInfo.instance.CashInfo(1).ToString();
    }
    public WeaponData ReturnWeaponDatas(int num)
    {
        return weaponDatas[num];
    }
    public void StartBtnOnClick()
    {
        SceneManager.LoadScene(1);
        ChangeScene.instance.SetArmies(chooseArmyGos, chooseArmyCount);
    }
    public void ArmyBtnOnClick()
    {
        if(weaponLevel >0)ChooseArmy(weaponNum);
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
    /*public void SwapBtnOnlick(int num)
    {
        for (int a = 0; a < themas.Length; a++)
        {
            if (a == num) themas[a].transform.localScale = new Vector3(1, 1, 1);
            else if (a != num) themas[a].transform.localScale = new Vector3(0, 0, 0);
        }
    }*/

}
