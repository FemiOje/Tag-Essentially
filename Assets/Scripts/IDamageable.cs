using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health{get; set;}
    int Speed{get; set;}
    int DamagePoints{get; set;}

    void TakeDamage(int damagePoints);
}
