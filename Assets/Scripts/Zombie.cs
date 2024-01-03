using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    private Animator _animator;

    void Update()
    {
        transform.position += (playerPos.position - transform.position) * 1.0f * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        if (transform.position == playerPos.position)
        {
            transform.position += Vector3.zero;
        }
    }
}
