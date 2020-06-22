using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack
{
    float currentAttackCD = 0f;
    [SerializeField] EnemyAI ai;
    [SerializeField] AnimationSetUp animationSetter;

    private void Update() {
        if(currentAttackCD >= 0f){
            currentAttackCD -= Time.deltaTime;
        }
    }

     public override void DoAttack()
     {
         if(currentAttackCD <= 0f){
            animationSetter.SetAttackAnim();
            ai.GetTargetHealth().TakeDamage(damage);
            currentAttackCD = attackCooldown;    
         }

     }
}
