using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] int pointsReward = 200;
    [SerializeField] float wanderSpeed = 2f;
    [Header("Combat Stats")]
    [SerializeField] float tauntRange = 12f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float attackRange = 10f;
    [Header("Settings")]
    [SerializeField] float timeBetweenAttacks = 0.75f;
    [SerializeField] bool canMove = true;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform legs;
    [SerializeField] Transform attackPoint;
    [SerializeField] AudioSource attackSource;
    [SerializeField] GameObject projectile;

    Rigidbody2D rb2d;
    Animator animator;

    GameObject target;
    bool isInAttackRange = false;
    bool playerSpotted = false;
    bool canWalkOnPlatform = true;
    bool hasTouchedTheWall = false;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isInAttackRange = CheckInRange();
        canWalkOnPlatform = Physics2D.Raycast(legs.position, Vector2.down, 0.5f, groundLayer);
        hasTouchedTheWall = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 0.25f, groundLayer);

        if (!playerSpotted && target == null && canMove && !IsViewClear())
        {
            Wander();
        }

        if (playerSpotted && !isInAttackRange && canMove && IsViewClear())
        {
            Chase();
        }

        if (isInAttackRange && canAttack && IsViewClear())
        {
            StartAttack();
        }
    }

    void Wander()
    {
        playerSpotted = CheckForPlayerInTauntRange();

        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(wanderSpeed * transform.localScale.x, rb2d.velocity.y);

        if (!canWalkOnPlatform || hasTouchedTheWall)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        }

        if (playerSpotted)
        {
            target = CheckForPlayerInTauntRange().transform.gameObject;
        }


    }

    private RaycastHit2D CheckForPlayerInTauntRange()
    {
        return Physics2D.Raycast(attackPoint.position, Vector2.right * transform.localScale.x, tauntRange, playerLayer);
    }

    private bool CheckInRange()
    {
        if (target == null) return false;

        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }

    void Chase()
    {
        animator.SetBool("isRunning", true);

        if (!canWalkOnPlatform)
        {
            target = null;
            playerSpotted = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
            return;
        }

        if (target == null) return;

        if (target.transform.position.x > transform.position.x)
        {
            rb2d.velocity = new Vector2(chaseSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-chaseSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }

    }

    void StartAttack()
    {
        canAttack = false;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        animator.SetBool("isRunning", false);
        rb2d.velocity = Vector2.zero;
        animator.SetTrigger("attack");
        InstantiateProjectile();
        attackSource.Play();
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    private void InstantiateProjectile()
    {
        GameObject go = Instantiate(projectile, attackPoint.position, Quaternion.identity);
        go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, 1);
    }

    public int GetPointsReward()
    {
        return pointsReward;
    }
    
    bool IsViewClear()
    {
        return !Physics2D.Raycast(attackPoint.position, Vector2.right * transform.localScale.x, attackRange, groundLayer);
    }
}
