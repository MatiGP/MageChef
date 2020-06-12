using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeChefAttack : Attack
{
    [SerializeField] Transform[] attackPoints;

    [SerializeField] GameObject projectile;
    [SerializeField] GameObject[] barrierAmmo;
    [SerializeField] float ammoRechargeTime;

    float currentAttackCooldown = 0f;
    int ammoLeft = 4;
    bool ammoRechargeCompleted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAttackCooldown >= 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
    }

    public override void DoAttack()
    {
        if (currentAttackCooldown <= 0f && ammoLeft > 0)
        {
            GameObject go = Instantiate(projectile, attackPoints[Random.Range(0, 3)].position, Quaternion.identity);
            go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z);          
            barrierAmmo[ammoLeft - 1].SetActive(false);
            ammoLeft--;
            currentAttackCooldown = attackCooldown;
        }
        if (ammoLeft == 0 && !ammoRechargeCompleted)
        {
            StartCoroutine(RechargeAmmo());
            
        }



    }

    IEnumerator RechargeAmmo()
    {
        ammoRechargeCompleted = true;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(ammoRechargeTime);
            barrierAmmo[i].SetActive(true);          
        }
        yield return new WaitForSeconds(0.5f);
        ammoLeft = 4;
        ammoRechargeCompleted = false;
    }
}
