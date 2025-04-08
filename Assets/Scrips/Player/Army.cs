using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Army : MonoBehaviour
{
    //Attack Info
    [SerializeField] WeaponData weaponData;
    [SerializeField] Image remainBulletImg;
    [Header("#Weapon Info")]
    private bool canAttack = false; //공격 가능 여부
    private float damage; 
    private float fireRate;
    private float tempFireRate;
    private float attackRange;
    private float maxbulletCount;
    private bool isReloading = false;
    private float remainBulletCount;
    private bool canPenetrate;
    private float reloadingTime;

    [SerializeField] private GameObject bulletGO;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private LayerMask enemyLayer;
    public Collider[] enemyColls;

    LineRenderer lineRenderer;
    private void Awake()
    {
        WeaponInfoInit();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        fireRate -= Time.deltaTime;

        remainBulletImg.fillAmount = (remainBulletCount/maxbulletCount);

        CheckEnemy();
    }
    private void Start()
    {
        
    }
    private void WeaponInfoInit()
    {
        damage = weaponData.damage;
        fireRate = weaponData.fireRate;
        tempFireRate = fireRate;
        attackRange = weaponData.range;
        maxbulletCount = weaponData.maxBulletCount;
        remainBulletCount = maxbulletCount;
        canPenetrate = weaponData.canPenetrate;
        reloadingTime = weaponData.reloadingTime;
    }
    private void AttackTest(EnemyCtrl enemy)
    {
        //Data
        fireRate = tempFireRate;
        remainBulletCount--;

        GameObject bullet = Instantiate(bulletGO, bulletSpawnPos.position, this.transform.rotation);
        bullet.GetComponent<BulletCtrl>().SetBulletInfo(damage,0);

        if (remainBulletCount <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
        remainBulletCount = maxbulletCount;
    }
    private void CheckEnemy()
    {
        enemyColls = null;
        enemyColls = Physics.OverlapSphere(this.transform.position, attackRange, enemyLayer);
        if(enemyColls.Length > 0)
        {

            GameObject enemyGO = FindClosestTarget(enemyColls).gameObject;
            transform.LookAt(enemyGO.transform.position);
            EnemyCtrl enemy = enemyGO.GetComponent<EnemyCtrl>();
            //if (fireRate <= 0.0f && !isReloading) Attack(enemy);
            if (fireRate <= 0.0f && !isReloading) AttackTest(enemy);

        }
    }
    private Collider FindClosestTarget(Collider[] targets)
    {
        float closestDist = Mathf.Infinity;
        Collider target = null;

        foreach (var entity in targets)
        {
            //vector3.distance 보다 sqrMagnitude의 계산이 더 빠름
            float distance = (entity.transform.position - this.transform.position).sqrMagnitude;
            if (distance<closestDist)
            {
                closestDist = distance;
                target = entity;
            }
        }
        return target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
