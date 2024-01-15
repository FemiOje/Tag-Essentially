using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int damagePoints;
    protected NavMeshAgent agent;
    [SerializeField] protected Player player;
    protected Rigidbody playerRb;
    [SerializeField] protected Transform playerTransform;
    protected Animator _animator;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("Please add a NavMesh Agent component to this gameObject");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogWarning("Please add an Animator component to this gameObject");
        }

        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            playerTransform = player.transform;
        }
    }

    protected virtual void Start()
    {
        _animator.SetTrigger("zombieWalk");
    }

    protected virtual void FixedUpdate()
    {
        agent.SetDestination(playerTransform.position);
    }

    protected abstract void Update();


    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            TakeDamage(damagePoints);

            if (health <= 0)
            {
                StartCoroutine(DestroyEnemy());
            }
        }

        if (other.gameObject.GetComponent<Player>())
        {
            AttackPlayer();
        }
    }


    protected virtual void AttackPlayer()
    {
        _animator.SetTrigger("walkToAttack");

        playerRb.velocity = Vector3.zero;
        playerRb.AddExplosionForce(10.0f, transform.position, 3.0f, 1.0f, ForceMode.Impulse);

        player.health -= damagePoints;
        Debug.Log(player.health);

        _animator.SetTrigger("attackToWalk");
    }

    protected virtual void TakeDamage(int damagePoints)
    {
        health -= damagePoints;
        if (damagePoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected IEnumerator DestroyEnemy()
    {
        _animator.ResetTrigger("zombieWalk");
        _animator.SetTrigger("fallBackward");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    protected void OnDisable()
    {
        _animator.enabled = false;
    }
}
