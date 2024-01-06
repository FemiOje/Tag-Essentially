using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with zombie");
        if (other.gameObject.GetComponent<Projectile>())
        {
            Debug.Log("Collision with bullet");
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
