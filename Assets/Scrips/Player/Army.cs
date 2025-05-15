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

    [Header("Audio #0 Pistol #1 Smg #2 AR #3 Sr #4 DMR #5 LMG")]
    [SerializeField] private AudioClip[] gunFireClips;
    [SerializeField] private AudioClip[] gunReloadingClips;

    private bool canAttack = false; //공격 가능 여부
    public float damage; 
    public float fireRate;
    public float reloadingTime;
    private float tempFireRate;
    public float attackRange;
    public float maxbulletCount; 
    public bool isReloading = false;
    private float remainBulletCount;
    
    public int weaponType;
    public int weaponGrade;

    [SerializeField] private GameObject bulletGO;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private LayerMask enemyLayer;
    private Collider[] enemyColls;
    [SerializeField] private int posNum;

    LineRenderer lineRenderer;
    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        WeaponInfoInit();
        GameManager.instance.SetHaveWeaponType(weaponType);
        UpdateSFX();
    }
    private void Update()
    {
        fireRate -= Time.deltaTime;

        remainBulletImg.fillAmount = (remainBulletCount/maxbulletCount);

        CheckEnemy();
    }
    private void UpdateSFX()
    {
        for(int a = 0; a < GameManager.instance.gunFireClips.Length; a++)
        {
            gunFireClips[a] = GameManager.instance.gunFireClips[a];
        }
        for (int a = 0; a< GameManager.instance.gunReloadingClips.Length; a++)
        {
            gunReloadingClips[a] = GameManager.instance.gunReloadingClips[a];
        }
    }
    public void IncreaseMaxBullet(int amount)
    {
        maxbulletCount += amount;
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
            case (WeaponData.WeaponGrade.God):
                weaponGrade = 5;
                break;
        }
    }
    public void Upgrade()
    {
        /*damage *= SkillManager.instance.GetWeaponData(weaponType)[0]/100;
        tempFireRate *= SkillManager.instance.GetWeaponData(weaponType)[1]/100;
        reloadingTime *= SkillManager.instance.GetWeaponData(weaponType)[2]/100;*/
    }
    private void Attack(EnemyCtrl enemy)
    {
        //Data
        fireRate = tempFireRate* SkillManager.instance.GetWeaponData(weaponType)[1] / 100;
        remainBulletCount--;

        //사운드 생성
        AudioClip fireClip = GetComponent<AudioClip>();
        if (weaponData.type == WeaponData.WeaponType.Pistol) fireClip = gunFireClips[0];
        else if (weaponData.type == WeaponData.WeaponType.SMG) fireClip = gunFireClips[1];
        else if (weaponData.type == WeaponData.WeaponType.Rifle) fireClip = gunFireClips[2];
        else if (weaponData.type == WeaponData.WeaponType.SR) fireClip = gunFireClips[3];
        else if (weaponData.type == WeaponData.WeaponType.DMR) fireClip = gunFireClips[4];
        else if (weaponData.type == WeaponData.WeaponType.Special) fireClip = gunFireClips[5];
        if (!isSrArmy)
        {
            SoundManager.instance.PlaySound(fireClip);

            /*GameObject bullet = ObjectPool.instance.MakeObj("bullet");
            bullet.transform.position = bulletSpawnPos.position;
            bullet.transform.rotation = this.transform.rotation;*/
            GameObject bullet = Instantiate(bulletGO, bulletSpawnPos.position,this.transform.rotation);
            bullet.GetComponent<BulletCtrl>().SetBulletInfo(damage * SkillManager.instance.GetWeaponData(weaponType)[0] / 100
                , (int)SkillManager.instance.GetWeaponData(weaponType)[3]);
        }
        else
        {
            SoundManager.instance.PlaySound(fireClip);

            /*GameObject srBullet = ObjectPool.instance.MakeObj("srBullet");
            srBullet.transform.position = bulletSpawnPos.position;
            srBullet.transform.rotation = Quaternion.Euler(0, 0, 0);
            srBullet.transform.rotation = this.transform.rotation;*/
            GameObject srBullet = Instantiate(bulletGO, bulletSpawnPos.position, this.transform.rotation);
            srBullet.GetComponent<SrBulletCtrl>().SetBulletInfo(damage * SkillManager.instance.GetWeaponData(weaponType)[0]/100, 10);
        }
        

        if (remainBulletCount <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    public int ReturnWeaponType()
    {
        Debug.Log(this.name+" : " + weaponType);
        return weaponType;
    }
    private IEnumerator Reload()
    {
        //총기에 따라 사운드 변경 하기
        SoundManager.instance.PlaySound(gunReloadingClips[0]);

        isReloading = true;
        yield return new WaitForSeconds(reloadingTime * SkillManager.instance.GetWeaponData(weaponType)[2] / 100);
        isReloading = false;
        remainBulletCount = maxbulletCount;
    }
    public int ReturnArmyWeaponData()
    {
        return weaponData.weaponNum;
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
                this.transform.LookAt(enemyGO.transform.position);
                EnemyCtrl enemy = enemyGO.GetComponent<EnemyCtrl>();
                //공격
                if (fireRate <= 0.0f && !isReloading) Attack(enemy);
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
