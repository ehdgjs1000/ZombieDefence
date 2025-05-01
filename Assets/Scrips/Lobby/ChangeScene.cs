using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;

    public int chooseArmyCount = 0;
    [SerializeField] private Army[] armies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetArmies(Army[] _armies, int _chooseArmyCount)
    {
        chooseArmyCount = _chooseArmyCount;
        int a = 0;
        while (a < armies.Length)
        {
            if (_armies[a] != null)
            {
                armies[a] = _armies[a];
                a++;
            }
            else if (_armies[a] == null) break;
        }
    }
    public Army GetArmy(int num)
    {
        return armies[num];
    }
    public int ArmyCount()
    {
        return armies.Length;
    }

}
