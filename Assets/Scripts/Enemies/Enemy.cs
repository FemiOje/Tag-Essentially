using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int damagePoints;
    protected NavMeshAgent agent;
    [SerializeField] protected Transform playerTransform;


    protected abstract void Update();

    protected virtual void Awake()
    {
       agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void FixedUpdate()
    {
        agent.SetDestination(playerTransform.position);
    }

    protected virtual void AttackPlayer(Player player)
    {
        player.health -= damagePoints;
        Debug.Log("Player has taken hit");
    }

    protected virtual void TakeDamage(int damagePoints)
    {
        health -= damagePoints;
        if (damagePoints <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log($"{name} has taken hit. {name} health: {health}");
    }
}
