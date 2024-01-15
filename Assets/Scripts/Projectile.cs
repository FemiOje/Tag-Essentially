using System.Collections;
using System.Collections.Generic;
using AutoLevel.Examples;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform player;

    void FixedUpdate()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(transform.forward * 10.0f * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter() {
        Destroy(gameObject);
    }
}
