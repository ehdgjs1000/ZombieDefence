using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{
    [SerializeField] private Transform initPos;
    [SerializeField] private GameObject[] wingGos;
    public float speed;
    private float moveTime = 7.0f;
    public bool isIngamePlane = false;
    

    // Update is called once per frame
    void Update()
    {
        moveTime -= Time.deltaTime;
        if (moveTime <= 0.0f)
        {
            moveTime = 7.0f;
            transform.position = initPos.position;
        }
        if(!isIngamePlane) this.transform.position += new Vector3(speed * 0.01f, 0, 0);
        else this.transform.position += new Vector3(0,0, speed * 0.01f);

        for (int a = 0; a<wingGos.Length; a++)
        {
            wingGos[a].transform.Rotate(new Vector3(0,0,600) * Time.deltaTime);

        }

    }
}
