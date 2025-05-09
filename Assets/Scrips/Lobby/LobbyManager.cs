using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{  
    public static LobbyManager instance;

    public GameObject[] themas;
    public int themaNum = 0;
    //Setting Datas
    [SerializeField] private Army[] armyGos;
    [SerializeField] public Army[] chooseArmyGos;
    [SerializeField] private Image[] chooseArmyGosSprites;
    [SerializeField] private Sprite[] armySprites;
    private int chooseArmyCount = 0;

    //WeaponDatas
    [SerializeField] private WeaponData[] weaponDatas;
    private WeaponData[] tempWeaponDatas = new WeaponData[14];
    public int weaponNum=0;
    public int weaponLevel;

    //AccountInfo
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text CrystalTxt;
    [SerializeField] private Text steminaTxt;
    [SerializeField] private Text levelTxt;

    [SerializeField] private GameObject propsGO;
    [SerializeField] private GameObject chapterClearGo;
    private bool chapterClearGoActive = false;
    [SerializeField] private GameObject QuestGo;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Image[] borderImgs;
    [SerializeField] private GameObject steminaPanel;
    [SerializeField] private GameObject steminaChargeCountTxt;
    private float steminaChargeCount;

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
        BackEndGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);

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
        BackEndGameData.Instance.GameDataLoad();
        //SetArmies();

        //저장된 army 세팅
        if (ChangeScene.instance.chooseArmyCount > 0)
        {
            for (int a = 0; a < ChangeScene.instance.chooseArmyCount; a++)
            {
                ChooseArmy(ChangeScene.instance.GetArmy(a).ReturnArmyWeaponData());
            }
        }
    }
    private void Update()
    {
        SynchAccountToLobby();
        SteminaCharging();
    }
    public void UpdateGameData()
    {
        BackEndGameData.Instance.GameDataUpdate();
        levelTxt.text = "Lv." + $"{BackEndGameData.Instance.UserGameData.level}";
        steminaTxt.text = $"{BackEndGameData.Instance.UserGameData.energy}";
        goldTxt.text = $"{BackEndGameData.Instance.UserGameData.gold}";
        CrystalTxt.text = $"{BackEndGameData.Instance.UserGameData.crystal}";
        AccountInfo.instance.Gold = BackEndGameData.Instance.UserGameData.gold;
        AccountInfo.instance.Crystal = BackEndGameData.Instance.UserGameData.crystal;
    }
    public void GetStemina(int amount)
    {
        BackEndGameData.Instance.UserGameData.energy += amount;
        UpdateGameData();
    }
    public void LoseStemian(int amount)
    {
        BackEndGameData.Instance.UserGameData.energy -= amount;
        UpdateGameData();
    }
    public void SetArmies()
    {
        if(ChangeScene.instance.chooseArmyCount > 0)
        {
            for (int a = 0; a < ChangeScene.instance.ArmyCount(); a++)
            {
                ChooseArmy(ChangeScene.instance.GetArmy(a).ReturnArmyWeaponData());
            }
        }
        
    }
    //사용 했던 Weapon저장 용도
    private void SteminaCharging()
    {
        if (BackEndGameData.Instance.UserGameData.energy >= 30)
        {
            steminaChargeCount = 1000; //스테미나 충전 시간
            //steminaChargeCountTxt.SetActive(false);
        }
        else
        {
            steminaChargeCount -= Time.deltaTime;
            //steminaChargeCountTxt.SetActive(true);
        }
    }
    public void SteminaBuyOnClick(int amount)
    {
        if (amount == 5)
        {
            if (BackEndGameData.Instance.UserGameData.energy <= 25)
            {
                //보상 다 본 후
                BackEndGameData.Instance.UserGameData.energy += 5;
                UpdateGameData();
            }
        }
        else if (amount == 30)
        {
            if (BackEndGameData.Instance.UserGameData.crystal >= 200 &&
                BackEndGameData.Instance.UserGameData.energy < 30)
            {
                BackEndGameData.Instance.UserGameData.crystal -= 200;
                BackEndGameData.Instance.UserGameData.energy += 30;
                UpdateGameData();
            }
        }
    }
    public void SteminaBtnOnClick()
    {
        steminaPanel.SetActive(true);
    }
    public void SteminaExitOnClick()
    {
        steminaPanel.SetActive(false);
    }
    public void SettingBtnOnClick()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSettingOnClick()
    {
        settingPanel.SetActive(false);
    }
    public void HideProps()
    {
        propsGO.SetActive(false);
    }
    public void ShowProps()
    {
        propsGO.SetActive(true);
    }
    public WeaponData ReturnWeaponDatas(int num)
    {
        return weaponDatas[num];
    }
    public int ReturnWeaponDatasLength()
    {
        return weaponDatas.Length;
    }
    public void StartBtnOnClick()
    {
        //선택 총기 없을경우 예외처리
        if(BackEndGameData.Instance.UserGameData.energy >= 5 )
        {
            SceneManager.LoadScene(1);

            BackEndGameData.Instance.UserGameData.energy -= 5;
            UpdateGameData();

            QuestManager.instance.questCount[1]++;
            ChangeScene.instance.SetArmies(chooseArmyGos, chooseArmyCount);
        }
        else
        {
            //게임 실행 불가
        }
    }
    public void QuestCloseBtnOnClick()
    {
        QuestGo.SetActive(false);
        propsGO.SetActive(true);
    }
    public void QuestOpenBtnOnClick()
    {
        QuestGo.SetActive(true);
        propsGO.SetActive(false);
    }
    public void ChapterClearBtnOnClick()
    {
        chapterClearGoActive = true;
        propsGO.SetActive(!chapterClearGoActive);
        chapterClearGo.SetActive(chapterClearGoActive);
    }
    public void ChapterCloseBtnOnClick()
    {
        chapterClearGoActive = false;
        propsGO.SetActive(!chapterClearGoActive);
        chapterClearGo.SetActive(chapterClearGoActive);
    }
    
    public void SyncLobbyToAccount()
    {
        int pistolC = 0;
        int smgC = 0;
        int rifleC = 0;
        int srC = 0;
        int dmrC = 0;
        int specialC = 0;
        for (int a = 0; a < weaponDatas.Length; a++)
        {
            if (weaponDatas[a].type == WeaponData.WeaponType.Pistol)
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.pistolLevel[pistolC];
                weaponDatas[a].weaponCount = AccountInfo.instance.pistolCount[pistolC];
                pistolC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.SMG)
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.smgLevel[smgC];
                weaponDatas[a].weaponCount = AccountInfo.instance.smgCount[smgC];
                smgC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.Rifle)
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.rifleLevel[rifleC];
                weaponDatas[a].weaponCount = AccountInfo.instance.rifleCount[rifleC];
                rifleC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.SR)
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.srLevel[srC];
                weaponDatas[a].weaponCount = AccountInfo.instance.srCount[srC];
                srC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.DMR)
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.dmrLevel[dmrC];
                weaponDatas[a].weaponCount = AccountInfo.instance.dmrCount[dmrC];
                dmrC++;
            }
            else
            {
                weaponDatas[a].weaponLevel = AccountInfo.instance.specialLevel[specialC];
                weaponDatas[a].weaponCount = AccountInfo.instance.specialLevel[specialC];
                specialC++;
            }
        }
        Save.instance.SaveGameJson();

    }
    private void SynchAccountToLobby()
    {
        int pistolC = 0;
        int smgC = 0;
        int rifleC = 0;
        int srC = 0;
        int dmrC = 0;
        int specialC = 0;
        for (int a = 0; a < weaponDatas.Length; a++)
        {
            if(weaponDatas[a].type == WeaponData.WeaponType.Pistol)
            {
                AccountInfo.instance.pistolLevel[pistolC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.pistolCount[pistolC] = weaponDatas[a].weaponCount;
                pistolC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.SMG)
            {
                AccountInfo.instance.smgLevel[smgC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.smgCount[smgC] = weaponDatas[a].weaponCount;
                smgC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.Rifle)
            {
                AccountInfo.instance.rifleLevel[rifleC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.rifleCount[rifleC] = weaponDatas[a].weaponCount;
                rifleC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.SR)
            {
                AccountInfo.instance.srLevel[srC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.srCount[srC] = weaponDatas[a].weaponCount;
                srC++;
            }
            else if (weaponDatas[a].type == WeaponData.WeaponType.DMR)
            {
                AccountInfo.instance.dmrLevel[dmrC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.dmrCount[dmrC] = weaponDatas[a].weaponCount;
                dmrC++;
            }
            else
            {
                AccountInfo.instance.specialLevel[specialC] = weaponDatas[a].weaponLevel;
                AccountInfo.instance.specialCount[specialC] = weaponDatas[a].weaponCount;
                specialC++;
            }
        }
        
    }
    public void ArmyBtnOnClick()
    {
        if(weaponLevel >0)ChooseArmy(weaponNum);
    }
    public void ChooseArmyBtnOnClick(int num)
    {
        chooseArmyGos[num] = null;
        chooseArmyGosSprites[num].sprite = null;

        borderImgs[num].color = Color.black;
        borderImgs[num].GetComponentsInChildren<Image>()[1].color = Color.white;

        chooseArmyCount--;
    }
    private void ArmySpriteChange(int _a, int a)
    {
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
            case 5:
                chooseArmyGosSprites[a].GetComponentsInParent<Image>()[2].color = Color.red;
                ColorUtility.TryParseHtmlString("#D10000", out color);
                chooseArmyGosSprites[a].GetComponentsInParent<Image>()[1].color = color;
                break;
        }
    }
    public void ChooseArmy(int _a)
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
                
                if(themaNum == 2)ArmySpriteChange(_a, a);
                
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
        themaNum = num;
    }

}
