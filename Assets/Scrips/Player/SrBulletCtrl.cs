using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrBulletCtrl : MonoBehaviour
{
    Rigidbody rigid;
    public float damage;
    [SerializeField] private float bulletSpeed;
    private int penetrateCount = 0;
    private bool canPenetrate = false;

    [SerializeField] private Transform damagePopUpTr;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        //rigid.velocity = transform.forward * bulletSpeed;

        rigid.AddForce(transform.forward*bulletSpeed);
        Destroy(gameObject, 4.0f);
    }

    public void SetBulletInfo(float dmg, int _penetrateCount)
    {
        damage = dmg;
        penetrateCount = _penetrateCount;
    }
    private void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag("Enemy"))
        {
            co.GetComponent<EnemyCtrl>().GetAttack(damage);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision co)
    {
        try
        {
            if (co.gameObject.CompareTag("Enemy"))
            {
                co.gameObject.GetComponent<EnemyCtrl>().GetAttack(damage);
                //Damage PopUp
                DamagePopUp.Create(this.transform.position, damage);
            }
        }
        catch (System.ObjectDisposedException e)
        {
            System.Console.WriteLine("Caught: {0}", e.Message);
        }

    }

}
