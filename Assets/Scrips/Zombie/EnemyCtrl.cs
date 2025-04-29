using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private bool canMove = true;
    private bool isDie = false;
    private bool isAttacking = false;
    private float attackCount = 0.0f;

    //Enemy Info
    [SerializeField] private bool isLongRange;
    [SerializeField] private float hp;
    [SerializeField] private float exp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private int gold;
    [SerializeField] private LayerMask armyLayer;

    //Animation
    private Animator animator;
    private void Awake()
    {
        //몬스터 체력 설정
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        float ranHpRate = Random.Range(0.7f, 1.3f);
        hp = hp * GameManager.instance.gameLevel * ranHpRate;
    }
    private void Update()
    {
        attackCount -= Time.deltaTime;

        if (canMove && !GameManager.instance.isStopGame) Move();
        if (hp <= 0.0f && !isDie) StartCoroutine(EnemyDie());

        if(attackCount <= 0.0f) AttackCheck();

    }
    public void InitHp(float hpTimes)
    {
        hp *= hpTimes;
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
        StartCoroutine(GameManager.instance.ArmyGetAttack(damage));
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
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        isDie = true;
        canMove = false;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }
    private void Move()
    {
        this.transform.position += new Vector3(0,0,-0.01f*moveSpeed);
    }

}
