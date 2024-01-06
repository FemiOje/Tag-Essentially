using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy, IDamageable
{
    public int Health { get; set; }
    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Zombie: Attacking");
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damagePoints);
            Debug.Log($"Zombie has taken hit. Zombie health: {health}");
        }
    }

    // private void OnCollisionEnter(Collision obj)
    // {

    //     if (obj.TryGetComponent(out IDamageable hit))
    //     {
    //         hit.Damage();
    //     }
    // }

    public void Damage()
    {
        Health--;
    }
}
