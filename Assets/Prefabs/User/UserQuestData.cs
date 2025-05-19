using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserQuestData
{
    public float[] questCount = new float[6];
    public bool[] isReceived = new bool[7];
    public bool[] isRewardReceived = new bool[5];
    public float questClearAmount = 0;

    public void Reset() // 매일 09:00에 초기화 하기
    {
        for(int a = 0; a < questCount.Length; a++)
        {
            questCount[a] = 0;
            Debug.Log(questCount[a]);
        }
        for(int b = 0; b < isReceived.Length; b++)
        {
            isReceived[b] = false;  
        }
        for(int c = 0; c < isRewardReceived.Length; c++){
            isRewardReceived[c] = false;
        }
        questClearAmount = 0;
        BackEndGameData.Instance.GameDataUpdate();
    }
}
