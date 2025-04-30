using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPool : MonoBehaviour
{
    public GameObject lobbyZombiePrefabs;

    GameObject[] zombies;

    GameObject[] targetPool;

    private void Awake()
    {
        zombies = new GameObject[100];

        Generate();
    }
    private void Generate()
    {
        for (int index = 0; index < zombies.Length; index++)
        {
            zombies[index] = Instantiate(lobbyZombiePrefabs);
            zombies[index].SetActive(false);
        }


    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "zombie":
                targetPool = zombies;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
    public GameObject DestroyObj()
    {
        for (int index = 0; index < targetPool.Length; index++)
        {
            targetPool[index].SetActive(false);
        }
        return null;
    }
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "zombie":
                targetPool = zombies;
                break;
        }
        return targetPool;
    }


}
