using System.Collections;
using System.Collections.Generic;
using AutoLevel.Examples;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(player.forward * 10.0f * Time.deltaTime);
    }
}
