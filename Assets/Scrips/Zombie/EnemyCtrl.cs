using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private bool canMove = true;
    private bool isDie = false;
    private bool isAttacking = false;
    private float attackCount = 0.0f;
    BoxCollider boxCollider;

    [Header("Zombie Info")]
    [SerializeField] private int zombieType;
    [SerializeField] private bool isLongRange;
    [SerializeField] private float hp;
    private float initHp;
    [SerializeField] private float exp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private int gold;
    [SerializeField] private LayerMask armyLayer;

    [SerializeField] private GameObject[] items;

    //Animation
    private Animator animator;
    private void Awake()
    {
        //몬스터 체력 설정
        initHp = hp;
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        float ranHpRate = Random.Range(0.7f, 1.3f);
        hp = initHp * GameManager.instance.gameLevel * ranHpRate;
    }
    private void Update()
    {
        attackCount -= Time.deltaTime;

        if (canMove && !GameManager.instance.isStopGame) Move();
        if (hp <= 0.0f && !isDie) StartCoroutine(EnemyDie());

        if(attackCount <= 0.0f) AttackCheck();
    }
    public IEnumerator ReUseZombie()
    {
        yield return new WaitForSeconds(0.2f);
        float ranHpRate = Random.Range(0.7f, 1.3f);
        hp = initHp * GameManager.instance.gameHpLevel * ranHpRate;
        if (ZombieSpawner.instance.isHardMode) hp *= 1.3f;
        isDie = false;
        canMove = true;
        boxCollider.enabled = true;
    }
    private void AttackCheck()
    {
        Physics.Raycast(this.transform.position, transform.forward,
            out RaycastHit hitInfo,attackRange, armyLayer);
        if(hitInfo.collider != null && !isAttacking)
        {
            StartCoroutine(Attack(damage));
        }
        
    }
    IEnumerator Attack(float damage)
    {
        isAttacking = true;
        animator.SetTrigger("ShortAttack");
        yield return new WaitForSeconds(1.5f);
        if(!isDie) StartCoroutine(GameManager.instance.ArmyGetAttack(damage));
        isAttacking = false;
    }
    public void GetAttack(float damage)
    {
        hp -= damage;
    }
    private IEnumerator EnemyDie()
    {
        GameManager.instance.GainExp(exp);
        GameManager.instance.GetGold(gold);
        GameManager.instance.KilledZombie(zombieType);
        BackEndGameData.Instance.UserQuestData.questCount[2]++;

        float ranBomb = Random.Range(0.0f, 100.0f);
        if (ranBomb <= 0.2f) SpawnItem();

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        isDie = true;
        canMove = false;

        int ranAnim = Random.Range(0,4);
        switch (ranAnim)
        {
            case 0:
                animator.SetTrigger("Die1");
                break;
            case 1:
                animator.SetTrigger("Die2");
                break;
            case 2:
                animator.SetTrigger("Die3");
                break;
            case 3:
                animator.SetTrigger("Die4");

                break;
        }
        SpawnItem();
        StartCoroutine(ObjectPool.instance.DeActive(10.0f, this.gameObject));
        yield return null;
    }
    private void SpawnItem()
    {
        int spawnItem = Random.Range(0,3);
        if(spawnItem == 1) 
        {
            
        }else if (spawnItem == 2)
        {

        }
        else
        {

        }

    }
    private void Move()
    {
        this.transform.position += new Vector3(0,0,-0.01f*moveSpeed);
    }

}
