using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int damagePoints;

    protected virtual void Attack(){
        Debug.Log("Enemy class: attacking");
    }

    protected virtual void TakeDamage(int damagePoints){
        health -= damagePoints;
    }

    protected abstract void Update();
}
