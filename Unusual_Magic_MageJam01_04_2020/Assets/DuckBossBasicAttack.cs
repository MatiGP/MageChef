using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBossBasicAttack : Attack
{
    [SerializeField] Transform[] attackPoints;
    [SerializeField] Animator animator;
    [SerializeField] GameObject projectile;

    float currentAttackCooldown = 0f;

    public override void DoAttack()
    {
        if (currentAttackCooldown <= 0f)
        {
            int randomIndex = Random.Range(0, attackPoints.Length);

            GameObject go = Instantiate(projectile, attackPoints[randomIndex].position, Quaternion.identity);
            go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z);
            currentAttackCooldown = attackCooldown;
            if(randomIndex == 0)
            {
                animator.SetTrigger("attackHigh");
            }
            else
            {
                animator.SetTrigger("attackLow");
            }

        }
    }

    void Update()
    {
        if (currentAttackCooldown >= 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }
}
