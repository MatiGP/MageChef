using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        health--;
        
        if(health <= 0)
        {
            if (tag == "Enemy")
            {
                PlayerPoints.instance.IncreasePoints(GetComponent<EnemyController>().GetPointsReward());
            }
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmgAmmount)
    {
        health = Mathf.Max(health, 0, health - dmgAmmount);
        if (health <= 0)
        {            
            if(tag == "Enemy")
            {
                PlayerPoints.instance.IncreasePoints(GetComponent<EnemyController>().GetPointsReward());
            }
            Destroy(gameObject);
        }
    }   

    public int GetHealthAmmount()
    {
        return health;
    }

    public void RestoreHealth()
    {
        health = Mathf.Min(4, health++);
    }
}
