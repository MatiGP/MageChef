using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointRangedAttack : Attack
{
    [SerializeField] Transform[] attackPoints;

    [SerializeField] GameObject projectile;

    float currentAttackCooldown = 0f;

    public override void DoAttack()
    {
        if (currentAttackCooldown <= 0f)
        {
            GameObject go = Instantiate(projectile, attackPoints[Random.Range(0, attackPoints.Length)].position, Quaternion.identity);
            go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z);           
            currentAttackCooldown = attackCooldown;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (currentAttackCooldown >= 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }

}
