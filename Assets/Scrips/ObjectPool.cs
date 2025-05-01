using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject bulletPrefabs;
    public GameObject srBulletPrefabs;

    GameObject[] bullets;
    GameObject[] srBullets;

    GameObject[] targetPool;

    private void Awake()
    {
        if(instance == null)instance = this;

        bullets = new GameObject[500];
        srBullets = new GameObject[500];

        Generate();
    }
    private void Generate()
    {
        for (int index = 0; index < bullets.Length; index++)
        {
            bullets[index] = Instantiate(bulletPrefabs);
            bullets[index].SetActive(false);
        }
        for (int index = 0; index < bullets.Length; index++)
        {
            srBullets[index] = Instantiate(srBulletPrefabs);
            srBullets[index].SetActive(false);
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
            case "zombie":
                targetPool = bullets;
                break;
        }
        return targetPool;
    }
}
