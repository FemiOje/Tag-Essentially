using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            TakeDamage(damagePoints);
            Destroy(other.gameObject);

            if (health <= 0){
                Destroy(gameObject);
            }
        }
    }
    protected override void Update()
    {
        
    }
}
