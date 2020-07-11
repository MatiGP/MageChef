using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookieMeleeAttack : Attack
{
    float currentAttackCD = 0f;
    WrestlingCookieAI cookieAI;

    private void Start()
    {
        cookieAI = GetComponent<WrestlingCookieAI>();
    }

    private void Update()
    {
        if (currentAttackCD >= 0f)
        {
            currentAttackCD -= Time.deltaTime;
        }
    }

    public override void DoAttack()
    {
        if (currentAttackCD <= 0f)
        {
            cookieAI.TriggerAnimationAttack();
            cookieAI.HurtTarget();
            onAttackAudio?.Play();
            currentAttackCD = attackCooldown;
            
        }
    }
}
