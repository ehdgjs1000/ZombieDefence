using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject bulletPrefabs;
    public GameObject srBulletPrefabs;
    public GameObject[] normalZombiePrefab;
    public GameObject[] fastZombiePrefab;
    public GameObject[] longZombiePrefab;
    public GameObject[] tankZombiePrefab;

    GameObject[] bullets;
    GameObject[] srBullets;
    GameObject[] normalZombies;
    GameObject[] fastZombies;
    GameObject[] longZombies;
    GameObject[] tankZombies;

    GameObject[] targetPool;

    private void Awake()
    {
        if(instance == null)instance = this;

        bullets = new GameObject[300];
        srBullets = new GameObject[300];
        normalZombies = new GameObject[200];
        fastZombies = new GameObject[200];
        longZombies = new GameObject[200];
        tankZombies = new GameObject[200];

        Generate();
    }
    private void Generate()
    {
        for (int index = 0; index < bullets.Length; index++)
        {
            bullets[index] = Instantiate(bulletPrefabs);
            bullets[index].SetActive(false);
        }
        for (int index = 0; index < srBullets.Length; index++)
        {
            srBullets[index] = Instantiate(srBulletPrefabs);
            srBullets[index].SetActive(false);
        }
        //Zombie Prefab Pool
        for (int index = 0; index < normalZombies.Length; index++)
        {
            normalZombies[index] = Instantiate(normalZombiePrefab[index%normalZombiePrefab.Length]);
            normalZombies[index].SetActive(false);
        }
        for (int index = 0; index < fastZombies.Length; index++)
        {
            fastZombies[index] = Instantiate(fastZombiePrefab[index%fastZombiePrefab.Length]);
            fastZombies[index].SetActive(false);
        }
        for (int index = 0; index < longZombies.Length; index++)
        {
            longZombies[index] = Instantiate(longZombiePrefab[index%longZombiePrefab.Length]);
            longZombies[index].SetActive(false);
        }
        for (int index = 0; index < longZombies.Length; index++)
        {
            tankZombies[index] = Instantiate(tankZombiePrefab[index % tankZombiePrefab.Length]);
            tankZombies[index].SetActive(false);
        }

    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "bullet":
                targetPool = bullets;
                break;
            case "srBullet":
                targetPool = srBullets;
                break;
            case "normalZombie":
                targetPool = normalZombies;
                break;
            case "fastZombie":
                targetPool = fastZombies;
                break;
            case "longZombie":
                targetPool = longZombies;
                break;
            case "tankZombie":
                targetPool = tankZombies;
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


     public IEnumerator DeActive(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.transform.position = Vector3.zero;
        go.gameObject.SetActive(false);
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
            case "bullet":
                targetPool = bullets;
                break;
        }
        return targetPool;
    }
}
