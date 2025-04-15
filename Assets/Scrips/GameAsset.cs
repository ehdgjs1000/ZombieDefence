using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    private static GameAsset _instance;
    
    public static GameAsset Instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load<GameAsset>("GameAsset"));
            return _instance;
        }
    }

    public Transform pfDamagePopUp;

}
