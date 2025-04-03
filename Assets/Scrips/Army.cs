using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    //Attack Info
    private bool canAttack = false; //공격 가능 여부
    [SerializeField] private float attackDamage; 
    [SerializeField] private float attackTerm;
    [SerializeField] private float reloadTime;
    [SerializeField] private float attackRange;

    [SerializeField] private LayerMask enemyLayer;
    private Collider[] enemyColls;

    private void Update()
    {
        CheckEnemy();
    }
    private void CheckEnemy()
    {
        enemyColls = null;
        enemyColls = Physics.OverlapSphere(this.transform.position, attackRange, enemyLayer);
        if(enemyColls.Length > 0)
        {
            GameObject enemyGO = enemyColls[0].gameObject;
            EnemyCtrl enemy = enemyGO.GetComponent<EnemyCtrl>();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
