using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Melee enemies;
public class GroundEnemyAI : MonoBehaviour
{
    [SerializeField] float groundEnemyMoveSpeed;
    [SerializeField] float groundEnemyTauntRange;
    [SerializeField] float groundEnemyChaseSpeed;
    [SerializeField] float groundEnemyAttackRange;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform attackPoint;

    [SerializeField] List<Ability> abilities;


    Rigidbody2D rb;
    Animator animator;
    GameObject target;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    private void Update()
    {
        target = CheckForTargetInTauntRange();

        if (target == null)
        {
            Wander();
        }
        else
        {
            if (IsTargetInAttackRange() || IsTargetInGapCloseRange())
            {
                Attack();
            }
            else
            {
                Chase();
            }                     
        }     
    }

    void Wander()
    {
        if (!HasReachedEndPath())
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        }

        rb.velocity = new Vector2(groundEnemyMoveSpeed * transform.localScale.x, rb.velocity.y);

    }

    void Chase()
    {
        if (HasReachedEndPath())
        {
            target = null;
            return;
        }

        if(target.transform.position.x > transform.position.x && (!IsTargetInAttackRange() || !IsTargetInGapCloseRange()))
        {
            rb.velocity = new Vector2(groundEnemyChaseSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
        else if (target.transform.position.x < transform.position.x && (!IsTargetInAttackRange() || !IsTargetInGapCloseRange()))
        {
            rb.velocity = new Vector2(-groundEnemyChaseSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }

        abilities[2].UseAbility();
    }

    void Attack()
    {
        if (IsTargetInAttackRange())
        {
            abilities[0].UseAbility();
        }else if (IsTargetInGapCloseRange())
        {
            abilities[1].UseAbility();
        }
    }

    GameObject CheckForTargetInTauntRange()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, groundEnemyTauntRange, playerLayer).transform.gameObject;
    }

    bool IsTargetInAttackRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) <= groundEnemyAttackRange;
    }

    bool HasReachedEndPath()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 4f, groundLayer);
    }

    bool IsTargetInGapCloseRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) <= abilities[0].abilityRange;
    }


}
