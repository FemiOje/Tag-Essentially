using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    // public int Health { get; set; }
    // public int Speed { get; set; }
    // public int DamagePoints { get; set; }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<Projectile>()){
            TakeDamage(damagePoints);
            
            if (health <= 0){
                Destroy(gameObject);
            }
        }
    }

    protected override void Update()
    {

    }

}
