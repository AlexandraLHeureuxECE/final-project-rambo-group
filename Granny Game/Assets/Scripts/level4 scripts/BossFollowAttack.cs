using System.Collections;
using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class BossAI : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 20;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float moveSpeed = 3.0f;
    private Animator _animator;
    private NavMeshAgent agent;
    private Transform player;
    private bool canAttack = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Make the boss follow the player
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the boss is within attack range, stop moving and attack
        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;  // Stop movement
            _animator.SetBool("RUN", false); // Stop walk animation

            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            // Move towards the player
            agent.isStopped = false;  
            agent.SetDestination(player.position);
            _animator.SetBool("RUN", true); // Play walk animation
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        Debug.Log("Boss attacks!");
        
        _animator.SetTrigger("AttackTrigger");  // ✅ Triggers attack animation
        yield return new WaitForSeconds(0.5f); // Small delay for animation sync

        // Try to damage the player
        Player playerHealth = player.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Boss took damage! Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}