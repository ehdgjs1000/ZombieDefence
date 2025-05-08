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

    public void Reset()
    {
        level = 1;
        exp = 0;
        gold = 100;
        crystal = 1000;
        energy = 30;
    }
}
