using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossZombie : MonoBehaviour
{
    private bool canMove = true;
    private bool isDie = false;
    private bool isAttacking = false;
    private float attackCount = 0.0f;
    BoxCollider boxCollider;
    Rigidbody rigid;

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
    [SerializeField] private Image bossHpImage;
    [SerializeField] private GameObject bossHpBg;

    [SerializeField] private GameObject[] items;

    //Animation
    private Animator animator;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        initHp = hp;
    }
    private void Update()
    {
        attackCount -= Time.deltaTime;

        bossHpImage.fillAmount = hp/initHp;

        if (hp <= 0.0f && !isDie) BossDie();
        if (attackCount <= 0.0f) AttackCheck();
    }
    private void FixedUpdate()
    {
        if (canMove && !GameManager.instance.isStopGame && canMove) Move();
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
            out RaycastHit hitInfo, attackRange, armyLayer);
        if (hitInfo.collider != null && !isAttacking)
        {
            canMove = false;
            StartCoroutine(Attack(damage));
        }

    }
    IEnumerator Attack(float damage)
    {
        canMove = false;
        isAttacking = true;
        animator.SetTrigger("ShortAttack");
        yield return new WaitForSeconds(1.5f);
        if (!isDie) StartCoroutine(GameManager.instance.ArmyGetAttack(damage));
        isAttacking = false;
    }
    public void GetAttack(float damage)
    {
        hp -= damage;
    }
    private void BossDie()
    {
        isDie = true;
        bossHpBg.SetActive(false);
        GameManager.instance.BossLevelUp();
        GameManager.instance.GetGold(gold);
        GameManager.instance.KilledZombie(zombieType);

        SpawnItem();

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        canMove = false;

        animator.SetTrigger("Die1");
        SpawnItem();
        GameManager.instance.sec += 1;
        ZombieSpawner.instance.isBossSpawn = false;
        ZombieSpawner.instance.isBossTime = false;
    }
    private void SpawnItem()
    {
        Ability.instance.bombCount++;
    }
    private void Move()
    {
        this.transform.position += new Vector3(0, 0, -0.01f * moveSpeed);
    }

}
