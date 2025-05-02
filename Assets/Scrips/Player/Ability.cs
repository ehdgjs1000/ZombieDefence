using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private int BombCount = 0;
    
    public void BombOnClick()
    {
        if(BombCount > 0)
        {
            //스킬 사용
            float ranX = Random.Range(-3.8f, 3.5f);
            float ranZ = Random.Range(-6.2f, 12.0f);

            StartCoroutine(SpawnBomb(ranX, ranZ));
        }
    }
    IEnumerator SpawnBomb(float posX, float posZ)
    {
        Vector3 pos = new Vector3 (posX, 0, posZ);
        yield return new WaitForSeconds(3.0f);

    }

}
