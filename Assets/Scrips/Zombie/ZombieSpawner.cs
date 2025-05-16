using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    [SerializeField] private GameObject[] normalZombies;
    [SerializeField] private GameObject[] fastZombies;
    [SerializeField] private GameObject[] tankerZombies;
    [SerializeField] private GameObject[] longZombies;
    [SerializeField] private GameObject[] bossZombies;

    //Spawner
    private float spawnInterval = 0.1f;
    public float[] minSpawnInterval;
    public float[] maxSpawnInterval;

    //Clock
    [SerializeField] private GameObject clockHand;
    [SerializeField] private AudioClip zombieScreamClip;
    [SerializeField] private TextMeshProUGUI timeTxt;
    public Light dirLight;
    private int time = 6;
    private float timerSec = 0.0f;
    private float sec = 0.0f;
    public float lightIntensity = 0.75f;
    private bool lightMode = true; //#false up #true down
    public bool isHardMode = false;

    //Obejcts
    [SerializeField] private GameObject SpotLight;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            StartCoroutine(SpawnZombies());
        }
        ClockUpdate();
    }
    private void ClockUpdate()
    {
        timerSec += Time.deltaTime;
        sec += Time.deltaTime;
        if(timerSec >= 60.0f)
        {
            timerSec = 0.0f;
        }
        if (sec >= 15.0f)
        {
            sec = 0.0f;
            time++;
            if (time == 24) time = 0;
            if(time == 22) ModeChange(1);
            else if (time == 3) ModeChange(0);
        }
        timeTxt.text = time.ToString()+":" + Mathf.FloorToInt(timerSec).ToString();
        clockHand.transform.Rotate(new Vector3(0,0,-1.0f) * Time.deltaTime);
        if (lightMode)
        {
            lightIntensity += (1.5f/180.0f)*Time.deltaTime;
            if (lightIntensity >= 1.5f) lightMode = !lightMode;
        }
        else
        {
            lightIntensity -= (1.5f / 180.0f) * Time.deltaTime;
            if (lightIntensity <= 0.0f) lightMode = !lightMode;
        }
        
        dirLight.intensity = lightIntensity;
    }
    //22시 3시 마다 모드 변경
    private void ModeChange(int modeType) //#0 Normal Mode #1 Hard Mode
    {
        Debug.Log("Mode Change");
        if (modeType == 1)
        {
            isHardMode = true;
            SpotLight.SetActive(true);
            SoundManager.instance.PlaySound(zombieScreamClip);
        }
        else
        {
            isHardMode = false;
            SpotLight.SetActive(true);
        }
    }
    IEnumerator SpawnZombies()
    {
        int gameLevel = GameManager.instance.gameLevel;
        if (gameLevel >= minSpawnInterval.Length) gameLevel = minSpawnInterval.Length;
        spawnInterval = Random.Range(minSpawnInterval[gameLevel], maxSpawnInterval[gameLevel]);
        //추후 레벨 시스템 도입하여 변경
        float ranX = Random.Range(-3.8f, 3.5f);
        float ranZ = Random.Range(17.0f, 19.0f);
        int ranZombie = Random.Range(0,4);
        switch (ranZombie)
        {
            case 0:
                GameObject zombieN = ObjectPool.instance.MakeObj("normalZombie");
                zombieN.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieN.transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(zombieN.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 1:
                GameObject zombieF = ObjectPool.instance.MakeObj("fastZombie");
                zombieF.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieF.transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(zombieF.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 2:
                GameObject zombieT = ObjectPool.instance.MakeObj("tankZombie");
                zombieT.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieT.transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(zombieT.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 3:
                GameObject zombieL = ObjectPool.instance.MakeObj("longZombie");
                zombieL.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieL.transform.rotation = Quaternion.Euler(0, 180, 0);
                StartCoroutine(zombieL.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
        }
        
        yield return null;  
    }

}
