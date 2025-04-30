using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyZombieCtrl : MonoBehaviour
{
    Animator animator;
    public float ranDieTime;
    public bool isDying = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ranDieTime = Random.Range(1.2f, 1.5f);
    }
    private void Update()
    {
        ranDieTime -= Time.deltaTime;
        if (ranDieTime <= 0.0f && !isDying)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        isDying = true;
        int dieType = Random.Range(0, 2);

        if (dieType == 0)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DeActive(4.0f, this.gameObject));
        }
        else
        {
            animator.SetTrigger("Die2");
            StartCoroutine(DeActive(4.0f, this.gameObject));
        }
        yield return null;
    }
    IEnumerator DeActive(float time, GameObject zombieGO)
    {
        yield return new WaitForSeconds(time);
        zombieGO.gameObject.SetActive(false);
    }

}
