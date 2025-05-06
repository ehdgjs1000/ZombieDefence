using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float exploseRange;
    [SerializeField] private float bombDamage;
    [SerializeField] private GameObject explosionFx;

    private Collider[] enemyColls;

    private void OnCollisionEnter(Collision co)
    {
        Explosion();
        if (co.transform.CompareTag("Ground"))
        {
            Debug.Log("Explose");
            
        }
    }
    private void Explosion()
    {
        enemyColls = null;
        enemyColls = Physics.OverlapSphere(this.transform.position, exploseRange, enemyLayer);
        if (enemyColls.Length > 0)
        {
            try
            {
                for (int a = 0; a < enemyColls.Length; a++)
                {
                    EnemyCtrl enemy = enemyColls[a].GetComponent<EnemyCtrl>();
                    enemy.GetAttack(bombDamage);
                }
                
            }

            catch (System.ObjectDisposedException e)
            {
                System.Console.WriteLine("Caught: {0}", e.Message);
            }
        }
        Instantiate(explosionFx, transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
