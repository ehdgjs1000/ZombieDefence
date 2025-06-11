using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KilledZombieCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nZombieText;
    [SerializeField] TextMeshProUGUI lZombieText;
    [SerializeField] TextMeshProUGUI fZombieText;
    [SerializeField] TextMeshProUGUI tZombieText; 
    [SerializeField] TextMeshProUGUI bZombieText;


    private void Update()
    {
        ZombieCountUpdate();
    }
    private void ZombieCountUpdate()
    {
        nZombieText.text = PlayerPrefs.GetInt("nZombieCount").ToString();
        lZombieText.text = PlayerPrefs.GetInt("lZombieCount").ToString();
        fZombieText.text = PlayerPrefs.GetInt("fZombieCount").ToString();
        tZombieText.text = PlayerPrefs.GetInt("tZombieCount").ToString();
        bZombieText.text = PlayerPrefs.GetInt("bZombieCount").ToString();
    }

}
