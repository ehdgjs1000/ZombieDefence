using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ability : MonoBehaviour
{
    public static Ability instance;

    public int bombCount = 1;
    [SerializeField] private GameObject bombGo;
    [SerializeField] private TextMeshProUGUI bombRemainTxt;
    [SerializeField] private GameObject noBombBg;
    
    private void Awake()
    {
        if (instance == null) instance = this;  
    }
    private void Update()
    {
        bombRemainTxt.text = bombCount.ToString();
        if(bombCount > 0 ) noBombBg.SetActive(false);
        else noBombBg.SetActive(true);
    }
    public void BombOnClick()
    {
        if(bombCount > 0)
        {
            bombCount--;
            float ranX = Random.Range(-3.2f, 3.2f);
            float ranZ = Random.Range(-6.2f, 6.0f);
            StartCoroutine(SpawnBomb(ranX, ranZ));
        }
    }
    IEnumerator SpawnBomb(float posX, float posZ)
    {
        Vector3 pos = new Vector3(posX, 10, posZ);
        yield return new WaitForSeconds(2.0f);

        Instantiate(bombGo, pos, Quaternion.Euler(90,0,0));
    }

}
