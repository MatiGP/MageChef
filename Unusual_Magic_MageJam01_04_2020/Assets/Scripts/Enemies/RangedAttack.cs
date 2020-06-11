using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    [SerializeField] GameObject projectile;
     public override void DoAttack()
     {
          Debug.Log("Pop");
     }   
}
