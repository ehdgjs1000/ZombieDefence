using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Army : MonoBehaviour
{
    //Attack Info
    [SerializeField] WeaponData weaponData;
    [SerializeField] Image remainBulletImg;
    [SerializeField] Image weaponImg;
    [SerializeField] private bool isSrArmy = false;
    [Header("#Weapon Info")]
    //#Upgrade Type
    //#0 Damge #1 fireRate #2 reloadingTime

    private bool canAttack = false; //공격 가능 여부
    public float damage; 
    private float fireRate;
    private float tempFireRate;
    private float attackRange;
    private float maxbulletCount;
    public bool isReloading = false;
    private float remainBulletCount;
    private float reloadingTime;
    private int weaponType;
    public int weaponGrade;

    [SerializeField] private GameObject bulletGO;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private LayerMask enemyLayer;
    public Collider[] enemyColls;
    [SerializeField] private int posNum;

    LineRenderer lineRenderer;
    private void Awake()
    {
        WeaponInfoInit();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        GameManager.instance.SetHaveWeaponType(weaponType);
    }
    private void Update()
    {
        fireRate -= Time.deltaTime;

        remainBulletImg.fillAmount = (remainBulletCount/maxbulletCount);

        CheckEnemy();
    }
    private void WeaponInfoInit()
    {
        damage = weaponData.damage;
        fireRate = weaponData.fireRate;
        tempFireRate = fireRate;
        attackRange = weaponData.range;
        maxbulletCount = weaponData.maxBulletCount;
        remainBulletCount = maxbulletCount;
        reloadingTime = weaponData.reloadingTime;
        weaponImg.sprite = weaponData.weaponImage;
        switch (weaponData.type)
        {
            case (WeaponData.WeaponType.Pistol):
                weaponType = 0;
                break;
            case (WeaponData.WeaponType.SMG):
                weaponType = 1;
                break;
            case (WeaponData.WeaponType.Rifle):
                weaponType = 2;
                break;
            case (WeaponData.WeaponType.SR):
                weaponType = 3;
                break;
            case (WeaponData.WeaponType.DMR):
                weaponType = 4;
                break;
            case (WeaponData.WeaponType.Special):
                weaponType = 5;
                break;
        }
        switch (weaponData.grade)
        {
            case (WeaponData.WeaponGrade.Normal):
                weaponGrade = 0;
                break;
            case (WeaponData.WeaponGrade.Special):
                weaponGrade = 1;
                break;
            case (WeaponData.WeaponGrade.Epic):
                weaponGrade = 2;
                break;
            case (WeaponData.WeaponGrade.Hero):
                weaponGrade = 3;
                break;
            case (WeaponData.WeaponGrade.Legendary):
                weaponGrade = 4;
                break;
        }
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade Checked");
        /*damage *= SkillManager.instance.GetWeaponData(weaponType)[0]/100;
        tempFireRate *= SkillManager.instance.GetWeaponData(weaponType)[1]/100;
        reloadingTime *= SkillManager.instance.GetWeaponData(weaponType)[2]/100;*/
    }
    private void AttackTest(EnemyCtrl enemy)
    {
        //Data
        fireRate = tempFireRate* SkillManager.instance.GetWeaponData(weaponType)[1] / 100;
        remainBulletCount--;

        GameObject bullet = Instantiate(bulletGO, bulletSpawnPos.position, this.transform.rotation);
        if (!isSrArmy)
        {
            bullet.GetComponent<BulletCtrl>().SetBulletInfo(damage * SkillManager.instance.GetWeaponData(weaponType)[0] / 100, 1);
        }
        else
        {
            bullet.GetComponent<SrBulletCtrl>().SetBulletInfo(damage, 1);
        }
        

        if (remainBulletCount <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime * SkillManager.instance.GetWeaponData(weaponType)[2] / 100);
        isReloading = false;
        remainBulletCount = maxbulletCount;
    }
    private void CheckEnemy()
    {
        enemyColls = null;
        enemyColls = Physics.OverlapSphere(this.transform.position, attackRange, enemyLayer);
        if(enemyColls.Length > 0)
        {
            try
            {
                GameObject enemyGO = FindClosestTarget(enemyColls).gameObject;
                transform.LookAt(enemyGO.transform.position);
                EnemyCtrl enemy = enemyGO.GetComponent<EnemyCtrl>();
                //공격
                if (fireRate <= 0.0f && !isReloading) AttackTest(enemy);
            }
            
            catch (System.ObjectDisposedException e)
            {
                System.Console.WriteLine("Caught: {0}", e.Message);
            }

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
