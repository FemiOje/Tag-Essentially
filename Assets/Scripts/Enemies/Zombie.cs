using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    protected override void Start()
    {
        _animator.SetTrigger("zombieWalk");
    }
    protected override void Update()
    {
    }
}
