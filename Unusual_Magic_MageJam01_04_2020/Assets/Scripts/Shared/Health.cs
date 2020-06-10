﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] AudioSource hurtSource;
    [SerializeField] GameObject deathPanel;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        hurtSource.Play();
        health--;
        
        if(health <= 0)
        {                     
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmgAmmount)
    {
        hurtSource.Play();
        health = Mathf.Max(0, health - dmgAmmount);
        if (health <= 0)
        {  
            gameObject.SetActive(false);
        }
    }   

    public int GetHealthAmmount()
    {
        return health;
    }

    public void RestoreHealth()
    {
        health++;
        Mathf.Clamp(health, 0, 4);
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        if (tag == "Player")
        {
            if (deathPanel == null) return;
            deathPanel.SetActive(true);
        }
    }
}
