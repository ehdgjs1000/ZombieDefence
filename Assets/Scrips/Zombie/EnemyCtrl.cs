using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private bool canMove = true;
    private bool isDie = false;
    private bool isAttacking = false;
    private float attackCount = 0.0f;
    public bool isCrawl = false;
    private float screamTime = 3.0f;
    BoxCollider boxCollider;
    Rigidbody rb;

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

        if (isCrawl) animator.SetTrigger("Crawl");
        else animator.SetTrigger("Run");
    }
    private void Update()
    {
        attackCount -= Time.deltaTime;
        screamTime -= Time.deltaTime;
        if (!isDie)
        {
            if (screamTime <= 0.0f && !isCrawl) StartCoroutine(Scream());
            if (hp <= 0.0f) StartCoroutine(EnemyDie());
        }
        

        if(attackCount <= 0.0f) AttackCheck();
    }
    private void FixedUpdate()
    {
        if (canMove && !GameManager.instance.isStopGame && !isAttacking) Move();
    }
    public IEnumerator Scream()
    {
        screamTime = 3.0f;
        int ranScream = Random.Range(0,10);
        if(ranScream <= 0)
        {
            canMove = false;
            animator.SetTrigger("Scream");
            yield return new WaitForSeconds(3.0f);
            if(!isDie) canMove = true;
        }
        yield return null;
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
            canMove = false;
            StartCoroutine(Attack(damage));
        }
        
    }
    IEnumerator Attack(float damage)
    {
        isAttacking = true;
        if (!isLongRange) animator.SetTrigger("ShortAttack");
        else if (isLongRange) animator.SetTrigger("LongAttack");
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

        SpawnItem();

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
        int spawnItem = Random.Range(0,100);
        if(spawnItem == 1) 
        {
            Ability.instance.bombCount++;
        }

    }
    private void Move()
    {
        this.transform.position += new Vector3(0,0,-0.01f*moveSpeed);
    }

}
