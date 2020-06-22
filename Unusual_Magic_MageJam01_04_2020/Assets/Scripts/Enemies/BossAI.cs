using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] Health bossHealth;
    [SerializeField] Health target;
    [SerializeField] Attack[] attacks;
    [SerializeField] float chaseSpeed = 5f;

    [SerializeField] int nextPhaseThreshhold;
    [SerializeField] float teleportThreshhold = 10f;
    [SerializeField] Transform[] tpPositions;
    [SerializeField] GameObject tpParticle;
    int currentPhase = 0;

    Rigidbody2D rb2d;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.GetHealthAmmount() <= nextPhaseThreshhold)
        {
            currentPhase = 1;
            nextPhaseThreshhold = -1;
            GetComponent<DuckBossMicrowave>().enabled = true;
            
        }

        if (teleportThreshhold >= Vector2.Distance(transform.position, target.transform.position) && currentPhase == 0)
        {
            Instantiate(tpParticle, transform.position, Quaternion.identity);
            Teleport();
        }

        if(transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
        }

        if (currentPhase == 1)
        {
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            attacks[1].DoAttack();
        }
        else
        {
            if (attacks[0].attackRange <= Vector2.Distance(transform.position, target.transform.position))
            {
                Chase();
            }
            else
            {
                attacks[0].DoAttack();
                animator.SetBool("isRunning", false);
            }
        }

        
    }

    public void SetTarget(Health bossTarget)
    {
        target = bossTarget;
    }

    void Chase()
    {
        rb2d.velocity = new Vector2(chaseSpeed * transform.localScale.x, rb2d.velocity.y);
        animator.SetBool("isRunning", true);
        
    }

    void Teleport()
    {
        if(transform.localScale.x < 0)
        {
            transform.position = tpPositions[1].position;         
        }
        else
        {
            transform.position = tpPositions[0].position;          
        }
    }

    public Health GetTarget()
    {
        return target;
    }

}
