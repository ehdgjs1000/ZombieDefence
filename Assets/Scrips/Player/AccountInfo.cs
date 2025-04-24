using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo : MonoBehaviour
{
    public static AccountInfo instance;

    private int Gold = 1000;
    private int Crystal = 1000;

    //Gun Info
    [Header("Gun Info")]
    public int[] pistolCount;
    public int[] smgCount;
    public int[] rifleCount;
    public int[] srCount;
    public int[] dmrCount;
    public int[] pistolLevel;
    public int[] rifleLevel;
    public int[] smgLevel;
    public int[] srLevel;
    public int[] dmrLevel;


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
    }

    public int CashInfo(int type) //#0 Gold #1 Crystal
    {
        if (type == 0) return Gold;
        else if (type == 1) return Crystal;
        else return 999;
    }
    public void GetCash(int type, int amount)
    {
        if (type == 0) Gold += amount;
        else if (type == 1) Crystal += amount;
    }
    public void LoseCash(int type, int amount)
    {
        if (type == 0) Gold -= amount;
        else if (type == 1) Crystal -= amount;
    }


}
