using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        transform.rotation = Quaternion.Euler(transform.rotation.x, 90.0f, transform.rotation.z);
    }

    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
