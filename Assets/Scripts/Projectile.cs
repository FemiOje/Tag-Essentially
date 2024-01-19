using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTransform;
    private Transform bulletSpawnTransform;
    // private Transform playerTransform;
    private Vector3 playerForward;
    // private Vector3 playerForward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // bulletSpawnTransform = GameObject.Find("bulletSpawnPoint").transform;

        // Use the player's position and forward direction
        // Vector3 playerPosition = playerTransform.localPosition;

        // Set the projectile's position to the player's position
        // transform.position = bulletSpawnTransform.position;

        // Set the projectile's velocity based on the player's forward direction
        // rb.velocity = playerForward * speed;
        rb.velocity = Vector3.forward * speed;
    }
    private void OnEnable()
    {

    }
    private void Update()
    {
        // playerForward = playerTransform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
