using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 15f;
    public float attackRadius = 2f;
    public int damageAmount = 10;
    public float attackRate = 1f;
    public int health = 100;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private float nextAttackTime;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        nextAttackTime = 0f;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer <= attackRadius)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else
            {
                Chase(distanceToPlayer);
            }
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    void Chase(float distance)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);

        if (distance > attackRadius * 2)
        {
            navMeshAgent.speed = 2;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            navMeshAgent.speed = 5;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
    }

    void Attack()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("isAttacking");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        navMeshAgent.isStopped = true;
        animator.SetBool("isDead", true);
        // Optionally, disable or destroy the zombie after the death animation
        Destroy(gameObject, 5f); // Adjust time as needed
    }

    // This method can be called via Animation Events at the point of impact in the attack animation
    public void ApplyDamage()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
