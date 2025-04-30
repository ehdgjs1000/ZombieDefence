using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
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
        rigid.AddForce(transform.forward*bulletSpeed);
        StartCoroutine(ObjectPool.instance.DeActive(4.0f, this.gameObject));
    }

    public void SetBulletInfo(float dmg, int _penetrateCount)
    {
        damage = dmg;
        penetrateCount = _penetrateCount;
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

                penetrateCount--;
                if (penetrateCount <= 0)
                {
                    StartCoroutine(ObjectPool.instance.DeActive(0.0f, this.gameObject));
                    
                }

            }
        }
        catch (System.ObjectDisposedException e)
        {
            System.Console.WriteLine("Caught: {0}", e.Message);
        }
    }

}
