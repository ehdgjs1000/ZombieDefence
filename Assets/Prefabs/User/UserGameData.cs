using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserGameData
{
    public int level;
    public float exp;
    public int gold;
    public int crystal;
    public int energy;
    public int promotionType; //#0 freee #1 9,900 #2 27,000

    public void Reset()
    {
        level = 1;
        exp = 0;
        gold = 100;
        crystal = 1000;
        energy = 30;
        promotionType = 0;
    }
}
