using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBg : MonoBehaviour
{
    [SerializeField] private GameObject zombieElbow;
    private bool rotDir = false;
    private float rotTime = 1.5f;
    [SerializeField] private GameObject zombieHead;
    private bool h_RotDir = false;
    private float h_RotTime = 1.2f;

    private void Awake()
    {

    }
    private void Update()
    {
        RotateElbow();
        RotateHead();
        rotTime -= Time.deltaTime;
        h_RotTime -= Time.deltaTime;
        if(rotTime <= 0)
        {
            rotTime = 1.5f;
            rotDir = !rotDir;
        }
        if (h_RotTime<=0)
        {
            h_RotTime = 1.2f;
            h_RotDir = !h_RotDir;
        }
    }
    private void RotateElbow()
    {
        if (!rotDir) zombieElbow.transform.Rotate(new Vector3(0, 0, 90f) * Time.deltaTime);
        else zombieElbow.transform.Rotate(new Vector3(0, 0, -90f) * Time.deltaTime);
    }
    private void RotateHead()
    {
        if (!h_RotDir) zombieHead.transform.Rotate(new Vector3(0, 0, 60f) * Time.deltaTime);
        else zombieHead.transform.Rotate(new Vector3(0, 0, -60f) * Time.deltaTime);
    }

}
