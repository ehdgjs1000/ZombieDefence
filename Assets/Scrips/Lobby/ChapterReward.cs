using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterReward : MonoBehaviour
{
    [SerializeField] private bool isProm1 = false;
    [SerializeField] private bool isProm2 = false;
    public bool canGetProm1 = false;
    public bool canGetProm2 = false;
    [SerializeField] private GameObject cannotGetImg;

    private void Start()
    {
        PromCheck();
    }
    private void PromCheck()
    {
        //서버와 연동해서 데이터 가져오기

        if(canGetProm1)
        {
            cannotGetImg.SetActive(false);
            isProm1 = true;
        }
        if(canGetProm2)
        {
            cannotGetImg.SetActive(false);
            isProm2 = true;
        }
    }

}
