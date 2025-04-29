using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] zombies;

    [SerializeField] private Transform[] spawnPoses;
    private float ranSpwanTime = 0.5f;

    private void Update()
    {
        if (LobbyManager.instance.themas[1].activeSelf)
        {
            ranSpwanTime -= Time.deltaTime;
            if (ranSpwanTime < 0) SpawnZombie();
        }
        
    }

    private void SpawnZombie()
    {
        ranSpwanTime = Random.Range(0.1f,0.2f);
        int ranZombie = Random.Range(0, zombies.Length);
        int ranPos = Random.Range(0, spawnPoses.Length);
        GameObject zombie = Instantiate(zombies[ranZombie], spawnPoses[ranPos].position, Quaternion.Euler(0,-90,0));
        float ranTime = Random.Range(5.0f,9.0f);
        Destroy(zombie, ranTime);
    }

}
