using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isStopGame = false;
    private int min;
    private float sec;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI gameOverTimeTxt;
    private int speedUpType = 0;
    [SerializeField] private TextMeshProUGUI speedUpTxt;
    public float tempTimeScale = 1.0f;
    private int gold = 0;
    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverSet;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject settingGo;
    [SerializeField] private Button settingBtn;

    [Header("Level System")]
    [SerializeField] private Skill[] skills;
    [SerializeField] private SkillData[] skillsDatas;
    [SerializeField] private Image expImage;
    [SerializeField] private Image hpImage;
    [SerializeField] private TextMeshProUGUI hpTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private LevelUp uiLevelUp;
    private int level = 1;
    public float hp = 100.0f;
    private float needExp = 100.0f;
    private float gainedExp = 0.0f;
    public int gameLevel = 1;
    public float gameHpLevel = 1;
    private float gameLevelTime = 120.0f;
    private float gameHpLevelTime = 60.0f;

    [Header("Army info")]
    [SerializeField] private Transform[] armyPos;
    [SerializeField] private Army[] armies;
    [SerializeField] private Army[] armiesGO;
    public bool[] haveWeaponType; //#0 Pistol #1 SMG #2 Rifle #3 SR #4 DMR #5 Special
    public bool canUpgradeCheck = false;

    [Header("Killed Zombie Info")]
    //#0 normal #1 fast #2 long #3 tank #4 boss
    private int[] killedZombieInfo = new int[5] {0,0,0,0,0};
    [SerializeField] private Text normalZTxt;
    [SerializeField] private Text fastZTxt;
    [SerializeField] private Text longZTxt;
    [SerializeField] private Text tankZTxt;
    [SerializeField] private Text bossZTxt;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        for (int a = 0; a< System.Enum.GetValues(typeof(WeaponData.WeaponType)).Length; a++)
        {
            haveWeaponType[a] = false;
        }
        gold = 0;
        min = 0;
        sec = 0;
    }
    private void Start()
    {
        SkillLevelReset();
        ArmySet();
    }
    private void Update()
    {
        gameLevelTime -= Time.deltaTime;
        gameHpLevelTime -= Time.deltaTime;
        UpdateInfo();

        if(!isGameOver) Timer();

        if (gameLevelTime <= 0.0f) GameLevelUp();
        if (gameHpLevelTime <= 0.0f) GameHpLevelUp();
        if (hp <= 0.0f && !isGameOver) GameOver();

        //Upgrade check
        if (canUpgradeCheck)
        {
            int a = 0;
            while (a < armies.Length)
            {
                armiesGO[a].Upgrade();
                a++;
                if (a == ChangeScene.instance.chooseArmyCount) break;
            }
            canUpgradeCheck = false;
        }
    }
    public void KilledZombie(int type)
    {
        killedZombieInfo[type]++;
    }
    private void KilledZombieInfo()
    {
        normalZTxt.text = killedZombieInfo[0].ToString() + " 마리";
        fastZTxt.text = killedZombieInfo[1].ToString() + " 마리";
        longZTxt.text = killedZombieInfo[2].ToString() + " 마리";
        tankZTxt.text = killedZombieInfo[3].ToString() + " 마리";
        //bossZTxt.text = killedZombieInfo[4].ToString() + " 마리";
    }
    public void SettingBtnOnClick()
    {
        settingGo.SetActive(true);
        isStopGame = true;
        Time.timeScale = 0.0f;
    }
    public Army ReturnArmy(int num)
    {
        return armiesGO[num];
    }
    public void GetGold(int goldAmount)
    {
        gold += goldAmount;
    }
    public void SpeedUpBtnOnClick()
    {
        speedUpType++;
        if(speedUpType >= 3)speedUpType = 0;
        switch (speedUpType)
        {
            case 0:
                speedUpTxt.text = "x1";
                Time.timeScale = 1.0f;
                tempTimeScale = 1.0f;
                break;
            case 1:
                speedUpTxt.text = "x1.5";
                Time.timeScale = 1.5f;
                tempTimeScale = 1.5f;
                break;
            case 2:
                speedUpTxt.text = "x2";
                Time.timeScale = 2.0f;
                tempTimeScale = 2.0f;
                break;
        }
        
    }
    private void Timer()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min += 1;
            sec = 0;
        }
        timeTxt.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }
    private void GameHpLevelUp()
    {
        Debug.Log("GameLevel UP");
        gameHpLevelTime = 60.0f;
        gameHpLevel *= 1.3f;
    }
    public IEnumerator ArmyGetAttack(float damage)
    {
        //Apply Zombie Attack Speed
        yield return new WaitForSeconds(0.1f);    
        hp -= damage;
    }
    private void ArmySet()
    {
        int a = 0;
        while (a < armies.Length)
        {
            if (ChangeScene.instance.GetArmy(a) != null)
            {
                armies[a] = ChangeScene.instance.GetArmy(a);
                a++;
            }
            else if (ChangeScene.instance.GetArmy(a) == null) break;
        }
        int armyNum = 0;
        while (armyNum < armies.Length)
        {
            armiesGO[armyNum] = Instantiate(armies[armyNum], armyPos[armyNum].position, Quaternion.identity);
            armyNum++;
            if (armyNum == ChangeScene.instance.chooseArmyCount) break;
        }

    }
    public void SetHaveWeaponType(int weaponType)
    {
        haveWeaponType[weaponType] = true;
    }
    private void GameOver()
    {
        KilledZombieInfo();
        isGameOver = true;
        gameOverTimeTxt = timeTxt;
        int killedZombieCount = 0;
        bool canLevelUp = true;
        for (int a = 0; a < killedZombieInfo.Length; a++)
        {
            killedZombieCount += killedZombieInfo[a];
        }

        //뒤끝 서버 연동 및 레벨업
        //추후 레벨 필요 경험치 량에 따른 조정 필요!!
        BackEndGameData.Instance.UserGameData.exp += killedZombieCount;
        while (canLevelUp)
        {
            if (BackEndGameData.Instance.UserGameData.exp >= 100)
            {
                BackEndGameData.Instance.UserGameData.exp -= 100;
                BackEndGameData.Instance.UserGameData.level++;
            }
            else canLevelUp = false;
        }

        gameOverSet.SetActive(true);
        BackEndGameData.Instance.UserQuestData.questCount[1]++;
        BackEndGameData.Instance.UserQuestData.questCount[2] += killedZombieCount;
        BackEndGameData.Instance.GameDataUpdate(AfterGameOver);
    }
    private void AfterGameOver()
    {
        SkillLevelReset();
        BackEndGameData.Instance.UserGameData.gold += gold;
        AccountInfo.instance.SyncAccountToBackEnd();
        Time.timeScale = 0.0f;
    }
    public void ToLobbyBtnOnClick()
    {
        tempTimeScale = 1.0f;
        Time.timeScale = 1.0f;
        gameOverSet.SetActive(false);
        SceneManager.LoadScene(0);
    }
    private void GameLevelUp()
    {
        gameLevel++;
        gameLevelTime = 120.0f;
    }
    private void UpdateInfo()
    {
        if(gainedExp >= needExp) LevelUp();
        expImage.fillAmount = (gainedExp / needExp);
        hpImage.fillAmount = (hp/100.0f);
        levelTxt.text = "Lv." + level.ToString();
        hpTxt.text = hp.ToString();
        goldText.text = gold.ToString();
    }
    public void GainExp(float tempExp)
    {
        gainedExp += tempExp;
    }
    public void LevelUp()
    {
        level++;
        gainedExp -= needExp;
        needExp *= 1.4f;
        for (int a = 0; a < skills.Length; a++)
        {
            skills[a].SkillUpdate();
        }
        uiLevelUp.gameObject.SetActive(true);
        uiLevelUp.ShowLevelUp();
        StopGame();
    }
    public void SkillLevelReset()
    {
        for (int a = 0; a < skillsDatas.Length; a++)
        {
            skillsDatas[a].skillLevel = 0;
        }
    }

    public void StopGame()
    {
        isStopGame = true;
        Time.timeScale = 0.0f;
    }
    public void ResumeGame()
    {
        isStopGame = false;
        Time.timeScale = tempTimeScale;
    }

}
