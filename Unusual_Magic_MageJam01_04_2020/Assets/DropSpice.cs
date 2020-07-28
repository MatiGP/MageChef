using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpice : MonoBehaviour
{
    [SerializeField] Spice[] spiceToDrop;
    [SerializeField] GameObject drop;
    [SerializeField] GameObject particleEffect;

    Health enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.damageTakenEvent += EnemyHealth_damageTakenEvent;
    }

    private void EnemyHealth_damageTakenEvent(int currentHealth)
    {
        if(currentHealth <= 0)
        {
            DropSpiceWhenKilled();
        }
    }

    public void DropSpiceWhenKilled()
    {
        GameObject droppedSpice = Instantiate(drop, transform.position, Quaternion.identity);
        droppedSpice.GetComponent<SpiceHolder>().SetUpSpice(spiceToDrop[Random.Range(0, spiceToDrop.Length)]);

        Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
}
