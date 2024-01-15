using System.Collections;
using System.Collections.Generic;
using AutoLevel.Examples;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed = 10.0f;
    [SerializeField] private Transform player;

    void FixedUpdate()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(transform.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter() {
        Destroy(gameObject);
    }
}
