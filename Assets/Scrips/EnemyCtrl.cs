using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private bool canMove = true;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        if (canMove) Move();

    }

    private void Move()
    {
        this.transform.position += new Vector3(0,0,-0.01f*moveSpeed);
    }

}
