using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : Attack
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform attackPoint;

     float currentAttackCD = 0f;

     private void Update() {
        if(currentAttackCD >= 0f){
            currentAttackCD -= Time.deltaTime;
        }
    }

     public override void DoAttack()
     {
          if(currentAttackCD <=0f){
               GameObject go = Instantiate(projectile, attackPoint.position, Quaternion.identity);
               go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, 1);
               currentAttackCD = attackCooldown;
          }
          
     }   
}
