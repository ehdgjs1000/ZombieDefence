using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInfo : MonoBehaviour
{
    public static AccountInfo instance;

    private int Gold = 100000;
    private int Crystal = 100000;
    public int stemina = 15;

    //Gun Info
    [Header("Gun Info")]
    public float[] pistolCount;
    public float[] smgCount;
    public float[] rifleCount;
    public float[] srCount;
    public float[] dmrCount;
    public float[] specialCount;
    public int[] pistolLevel;
    public int[] rifleLevel;
    public int[] smgLevel;
    public int[] srLevel;
    public int[] dmrLevel;
    public int[] specialLevel;

    public bool isLogIn = true;
    public float[] questCount = new float[6];
    public bool[] isClear = new bool[7] {false, false, false, false, false, false, false};


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
    private void Update()
    {

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
