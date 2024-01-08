using System.Collections;
using System.Collections.Generic;
using AutoLevel.Examples;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        // transform.position += player.position *  10.0f * Time.deltaTime;
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * 10.0f * Time.deltaTime);
    }
}
