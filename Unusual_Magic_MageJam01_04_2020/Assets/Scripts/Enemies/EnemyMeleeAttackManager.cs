using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackManager : MonoBehaviour
{
    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;

    float ability1Cooldown;
    float ability2Cooldown;
    float currentAbility1Cooldown;
    float currentAbility2Cooldown;

    public void SetUp(Ability a1, Ability a2)
    {
        ability1 = a1;
        ability2 = a2;

        ability1Cooldown = a1.baseCooldown;
        ability2Cooldown = a2.baseCooldown;


    }

    private void Update()
    {
        if(currentAbility1Cooldown >= 0)
        {
            currentAbility1Cooldown -= Time.deltaTime;
        }

        if (currentAbility2Cooldown >= 0)
        {
            currentAbility2Cooldown -= Time.deltaTime;
        }
    }

    public bool Ability1Ready()
    {
        return currentAbility1Cooldown <= 0;
    }

    public bool Ability2Ready()
    {
        return currentAbility2Cooldown <= 0;
    }

}
