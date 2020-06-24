using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int pointsReward = 100;

    [Header("AI Settings")]
    
    [SerializeField] float wanderSpeed = 3f;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] float chaseTime = 5f;
    [SerializeField] float tauntRange = 3f;
    [SerializeField] float aiJumpForce = 4f;
    [SerializeField] float groundCheckDistance = 5f;
    [SerializeField] bool stationary;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform legs;
    [SerializeField] Attack attack;
    [SerializeField] Rect box;
    Rigidbody2D rb2d;
    AnimationSetUp animationSetter;

    Health target;

    bool isInAttackRange;
    bool canWalkFurther;
    bool hasTouchedWall;
    bool hasTouchedOtherEnemy;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animationSetter = GetComponent<AnimationSetUp>();
        box.width = tauntRange;
    }

    // Update is called once per frame
    void Update()
    {
        canWalkFurther = Physics2D.Raycast(legs.position, Vector2.down, groundCheckDistance, groundLayer);
        hasTouchedWall = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 0.25f, groundLayer);
        hasTouchedOtherEnemy = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 1f, enemyLayer);

        
        if (target == null && !stationary){
            Wander();
            return;
        }
        else if (stationary)
        {
            target = CheckForPlayerInSight();
            if(target == null)
            {
                return;
            }
        }

        isInAttackRange = Vector2.Distance(target.transform.position, transform.position) <= attack.attackRange ? true : false;

        if(!isInAttackRange && !stationary){
            Chase();           
        }
        else{
            StopMovement();           
            attack.DoAttack();         
        }

        FaceThePlayer();
    }

    void Wander(){
        
        float scaleX = transform.localScale.x;
        if(hasTouchedWall || !canWalkFurther || hasTouchedOtherEnemy)
        {
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
        }
        animationSetter.SetRunAnim(true);
        rb2d.velocity = new Vector2(wanderSpeed * scaleX, rb2d.velocity.y);

        target = CheckForPlayerInSight();
    }

    void Chase(){

        float scaleX = transform.localScale.x;
        if((hasTouchedWall || !canWalkFurther)){
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
            target = null;
            return;
        }

        if (hasTouchedOtherEnemy)
        {           
            Jump();
        }

        animationSetter.SetRunAnim(true);
        rb2d.velocity = new Vector2(chaseSpeed * scaleX, rb2d.velocity.y);
    }

    void StopMovement(){
        rb2d.velocity = Vector2.zero;
        animationSetter.SetRunAnim(false);
    }

    Health CheckForPlayerInSight(){
        
        RaycastHit2D raycastHit = Physics2D.BoxCast((Vector2)transform.position + box.center * transform.localScale.x, box.size, 0f, Vector2.right * transform.localScale.x,0f, playerLayer);
        if(raycastHit)
        {
            return raycastHit.collider.GetComponent<Health>();
        }
        else
        {
            return null;
        }        
    }

    public Health GetTargetHealth(){
        return target;
    }

    void FaceThePlayer()
    {
        float scaleX = transform.localScale.x;
        if(target.transform.position.x < transform.position.x && scaleX > 0)
        {
            scaleX *= -1;
        }
        if(target.transform.position.x > transform.position.x && scaleX < 0)
        {
            scaleX *= -1;
        }
        transform.localScale = new Vector3(scaleX, transform.localScale.y, 1);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + box.center, box.size);
    }
    

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, aiJumpForce);
    }

}
