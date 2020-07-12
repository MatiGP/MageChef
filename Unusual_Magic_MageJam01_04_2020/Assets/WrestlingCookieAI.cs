using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookieAI : MonoBehaviour
{
    [SerializeField] Attack[] wrestlingAttacks = new Attack[3];
    // 0 - Melee X
    // 1 - DropKick X
    // 3 - BaguetteBottom
    [SerializeField] Health target;
    [SerializeField] float speed;
    [SerializeField] int damageToTakeToSwitchAttack = 3;
    Rigidbody2D rb2d;
    Animator animator;
    Health health;
    bool isInAttackRange;
    int attackIndex = -1;
    bool dropKicking;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.damageTakenEvent += Health_damageTakenEvent;
        NewAttack();

    }

    private void Health_damageTakenEvent(int currentHealth)
    {
        if(currentHealth % damageToTakeToSwitchAttack == 0)
        {
            NewAttack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isInAttackRange = Vector2.Distance(transform.position, target.transform.position) < wrestlingAttacks[attackIndex].attackRange;

        if (!dropKicking)
        {
            Move();
        }
        
        FaceThePlayer();
        if (isInAttackRange)
        {
            wrestlingAttacks[attackIndex].DoAttack();
        }
        

    }

    void Move()
    {       
        if(target.transform.position.x > transform.position.x && !isInAttackRange)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            animator.SetBool("isRunning", true);
        }
        else if(target.transform.position.x < transform.position.x && !isInAttackRange)
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FaceThePlayer()
    {
        if (dropKicking) return;

        if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void DoAttack()
    {
        wrestlingAttacks[attackIndex].DoAttack();        
    }

    void NewAttack()
    {
        attackIndex += 1;
        GetComponent<Rigidbody2D>().WakeUp();
        if(attackIndex > 2)
        {
            attackIndex = 0;
        }
    }

    public void HurtTarget()
    {
        target.TakeDamage();
    }

    public void TriggerAnimationAttack()
    {
        animator.SetTrigger("attack");
    }
    public void TriggerAnimationAttack1()
    {
        animator.SetTrigger("attack1");
    }
    public void TriggerAnimationAttack2()
    {
        animator.SetTrigger("attack2");
    }
    public void TriggerAnimationJump()
    {
        animator.SetTrigger("jump");
    }

    public void Move(float xVector)
    {
        rb2d.velocity = new Vector2(xVector, rb2d.velocity.y) ;
    }

    public void FreezePositionY()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        dropKicking = true;
        animator.SetBool("isDropKicking", true);
    }

    public void UnFreezePositionY()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        dropKicking = false;
        animator.SetBool("isDropKicking", false);
    }
}
