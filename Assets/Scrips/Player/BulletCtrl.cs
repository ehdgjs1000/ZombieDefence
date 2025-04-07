using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    Rigidbody rigid;
    private float damage;
    [SerializeField] private float bulletSpeed;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        rigid.velocity = transform.forward * bulletSpeed;

        Destroy(gameObject, 4.0f);
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    private void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag("Enemy"))
        {
            co.GetComponent<EnemyCtrl>().GetAttack(damage);
            Destroy(this.gameObject);
        }
    }

}
