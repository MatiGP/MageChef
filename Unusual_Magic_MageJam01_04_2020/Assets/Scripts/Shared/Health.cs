using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;

    public void TakeDamage()
    {
        health--;
        
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
