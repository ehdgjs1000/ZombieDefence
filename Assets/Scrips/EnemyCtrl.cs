using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    private bool canMove = true;
    [SerializeField] private float moveSpeed;
    private bool isDie = false;

    //Enemy Info
    [SerializeField] private float hp;
    [SerializeField] private float exp;

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
        if (canMove && !GameManager.instance.isStopGame) Move();

        if (hp <= 0.0f && !isDie) StartCoroutine(EnemyDie());

    }
    public void GetAttack(float damage)
    {
        hp -= damage;
    }
    private IEnumerator EnemyDie()
    {
        GameManager.instance.GainExp(exp);
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
