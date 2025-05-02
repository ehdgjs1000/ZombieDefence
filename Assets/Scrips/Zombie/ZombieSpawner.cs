using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] normalZombies;
    [SerializeField] private GameObject[] fastZombies;
    [SerializeField] private GameObject[] tankerZombies;
    [SerializeField] private GameObject[] longZombies;
    [SerializeField] private GameObject[] bossZombies;

    //Spawner
    private float spawnInterval = 0.1f;
    public float[] minSpawnInterval;
    public float[] maxSpawnInterval;

    private void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            StartCoroutine(SpawnZombies());
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
                zombieN.GetComponent<EnemyCtrl>().InitHp(GameManager.instance.gameHpLevel);
                StartCoroutine(zombieN.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 1:
                GameObject zombieF = ObjectPool.instance.MakeObj("fastZombie");
                zombieF.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieF.transform.rotation = Quaternion.Euler(0, 180, 0);
                zombieF.GetComponent<EnemyCtrl>().InitHp(GameManager.instance.gameHpLevel);
                StartCoroutine(zombieF.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 2:
                GameObject zombieT = ObjectPool.instance.MakeObj("tankZombie");
                zombieT.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieT.transform.rotation = Quaternion.Euler(0, 180, 0);
                zombieT.GetComponent<EnemyCtrl>().InitHp(GameManager.instance.gameHpLevel);
                StartCoroutine(zombieT.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
            case 3:
                GameObject zombieL = ObjectPool.instance.MakeObj("longZombie");
                zombieL.transform.position = new Vector3(ranX, 0.5f, ranZ);
                zombieL.transform.rotation = Quaternion.Euler(0, 180, 0);
                zombieL.GetComponent<EnemyCtrl>().InitHp(GameManager.instance.gameHpLevel);
                StartCoroutine(zombieL.GetComponent<EnemyCtrl>().ReUseZombie());
                break;
        }
        
        yield return null;  
    }

}
