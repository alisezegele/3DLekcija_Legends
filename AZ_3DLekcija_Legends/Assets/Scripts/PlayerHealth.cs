using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private float hitInterval = 0.5f;
    [SerializeField] private int healthGainedPerLevel = 5;
    [SerializeField] private ParticleSystem bloodEffect;
    
    private float lastHitTime = 0;
    private int currentHealth;
    private int currentMaxHealth;
    private Animator animator;

    public static bool isAlive = true;
    void Awake()
    {
        currentHealth = startingHealth;
        currentMaxHealth = startingHealth;
        animator = GetComponent<Animator>();
        isAlive = true;
    }

    public float GetHealthRatio()
    {
        return (float) currentHealth / (float) currentMaxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon") && isAlive && Time.time - lastHitTime > hitInterval)
        {
            TakeDamage(5);
        }
    }
    public void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        currentHealth -= damage;
        Debug.Log("Current health: " + currentHealth);
        
        if (bloodEffect != null)
        {
            bloodEffect.Play();
        }
        
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isAlive = false;
            animator.SetTrigger("Death");
        }
    }
    public void OnLevelGained(int newLevel)
    {
        currentMaxHealth = startingHealth + (newLevel - 1) * healthGainedPerLevel;
        currentHealth = currentMaxHealth;
    }
}
