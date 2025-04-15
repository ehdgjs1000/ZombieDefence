using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isStopGame = false;

    [Header("#Spawner")]
    [SerializeField] private GameObject[] enemies;
    private float spawnInterval = 0.5f;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    //Level System
    [SerializeField] private Skill[] skills;
    [SerializeField] private SkillData[] skillsDatas;
    [SerializeField] private Image expImage;
    [SerializeField] private Image hpImage;
    [SerializeField] private Text levelTxt;
    [SerializeField] private LevelUp uiLevelUp;
    private int level = 1;
    private float hp = 100.0f;
    private float needExp = 100.0f;
    private float gainedExp = 0.0f;
    public int gameLevel = 1;
    private float gameLevelTime = 60.0f;

    //Army
    [SerializeField] private Transform[] armyPos;
    [SerializeField] private Army[] armies;
    [SerializeField] private Army[] armiesGO;
    public bool[] haveWeaponType; //#0 Pistol #1 SMG #2 Rifle #3 SR #4 DMR #5 Special
    public bool canUpgradeCheck = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        for (int a = 0; a< System.Enum.GetValues(typeof(WeaponData.WeaponType)).Length; a++)
        {
            haveWeaponType[a] = false;
        }
    }
    private void Start()
    {
        SkillLevelReset();
        ArmySet();
    }
    private void Update()
    {
        gameLevelTime -= Time.deltaTime;
        spawnInterval -= Time.deltaTime;
        if(spawnInterval <= 0.0f) SpawnEnemy();

        UpdateInfo();
        if(gameLevelTime<=0.0f) GameLevelUp();

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
    private void EndGame()
    {
        SkillLevelReset();
    }
    private void GameLevelUp()
    {
        gameLevel++;
        gameLevelTime = 60.0f;
    }
    private void UpdateInfo()
    {
        if(gainedExp >= needExp) LevelUp();
        expImage.fillAmount = (gainedExp / needExp);
        hpImage.fillAmount = (hp/100.0f);
        levelTxt.text = "Lv." + level.ToString();
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
        for (int a = 0; a < skills.Length; a++)
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
        Time.timeScale = 1.0f;
    }
    private void SpawnEnemy()
    {
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        //추후 레벨 시스템 도입하여 변경
        float ranX = Random.Range(-3.8f, 3.8f);
        float ranZ = Random.Range(17.0f, 19.0f);
        Instantiate(enemies[0],new Vector3(ranX,0.5f,ranZ), Quaternion.Euler(0,180,0));

    }


}
