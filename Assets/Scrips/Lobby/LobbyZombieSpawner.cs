using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyZombieSpawner : MonoBehaviour
{
    public LobbyPool lobbyPool;

    [SerializeField] private GameObject[] zombies;
    [SerializeField] private Transform[] spawnPoses;
    private float ranSpwanTime = 0.0f;

    private void Update()
    {
        if (LobbyManager.instance.themas[1].activeSelf)
        {
            ranSpwanTime -= Time.deltaTime;
            if (ranSpwanTime < 0) SpawnZombie();
        }else if (!LobbyManager.instance.themas[1].activeSelf)
        {
            lobbyPool.DestroyObj();
        }
        
    }

    private void SpawnZombie()
    {
        ranSpwanTime = Random.Range(0.1f,0.2f);
        int ranPos = Random.Range(0, spawnPoses.Length);

        GameObject zombie = lobbyPool.MakeObj("zombie");
        zombie.transform.position = spawnPoses[ranPos].transform.position;
        zombie.transform.rotation = Quaternion.Euler(0,-90,0);
        
        LobbyZombieCtrl zCtrl = zombie.GetComponent<LobbyZombieCtrl>();
        zCtrl.ranDieTime = Random.Range(1.2f, 1.5f);
        zCtrl.isDying = false;
    }

}
