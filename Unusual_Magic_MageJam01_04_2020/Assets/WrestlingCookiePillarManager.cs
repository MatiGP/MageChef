using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookiePillarManager : Attack
{
    [SerializeField] WrestlingCookiePillar[] wrestlingCookiePillars;

    float currentCooldown;
    bool canAttack = true;

    private void Update()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    public override void DoAttack()
    {
        if (canAttack)
        {
            StartCoroutine(Rise());
        }
    }

    IEnumerator Rise()
    {
        currentCooldown = attackCooldown;

        foreach(WrestlingCookiePillar pillar in wrestlingCookiePillars)
        {
            pillar.Rise();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(2f);

        foreach (WrestlingCookiePillar pillar in wrestlingCookiePillars)
        {
            pillar.Fall();
            yield return new WaitForSeconds(1f);
        }

        

    }

    

    
}
