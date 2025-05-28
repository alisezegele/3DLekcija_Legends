using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;
    [SerializeField] private float attackInterval = 1.5f;
    
    public Transform target;
    private float lastAttacktTime = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        swordCollider.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        swordCollider.enabled = true;
    }

    public void EndAttack()
    {
        swordCollider.enabled = false;
    }

    public void OnDeath()
    {
        Debug.Log(name + " HAS DIED!");
        isDead = true;
        agent.isStopped = false;
        StartCoroutine(RemoveEnemy());
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            animator.SetBool("Running", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Running", false);
            if (Time.time - lastAttacktTime > attackInterval)
            {
                lastAttacktTime = Time.time;
                animator.SetTrigger("Attack");
            }
        }
    }

    private IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
