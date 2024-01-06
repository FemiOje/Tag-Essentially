using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }
    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Skeleton: Attacking");
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TakeDamage(damagePoints);
            Debug.Log($"Skeleton has taken hit. Skeleton health: {health}");
        }
    }

    public void Damage()
    {

    }

}
