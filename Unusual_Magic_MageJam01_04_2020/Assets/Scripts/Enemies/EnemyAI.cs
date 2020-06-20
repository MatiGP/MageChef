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
    [SerializeField] bool stationary;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform legs;
    [SerializeField] Attack attack;
    [SerializeField] Rect box;
    Rigidbody2D rb2d;
    Animator animator;

    Health target;

    bool isInAttackRange;
    bool canWalkFurther;
    bool hasTouchedWall;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box.width = attack.attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        canWalkFurther = Physics2D.Raycast(legs.position, Vector2.down, 0.5f, groundLayer);
        hasTouchedWall = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 0.25f, groundLayer);

        

        if(target == null && !stationary){
            Wander();
            return;
        }

        isInAttackRange = Vector2.Distance(target.transform.position, transform.position) <= attack.attackRange ? true : false;

        if(!isInAttackRange && !stationary){
            Chase();
        }else{
            StopMovement();
            FaceThePlayer();
            animator.SetTrigger("attack");
        }
    }

    void Wander(){
        
        float scaleX = transform.localScale.x;
        if(hasTouchedWall || !canWalkFurther){
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
        }
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(wanderSpeed * scaleX, rb2d.velocity.y);

        target = CheckForPlayerInSight();
    }

    void Chase(){

        float scaleX = transform.localScale.x;
        if(hasTouchedWall || !canWalkFurther){
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
            target = null;
            return;
        }
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(chaseSpeed * scaleX, rb2d.velocity.y);
    }

    void StopMovement(){
        rb2d.velocity = Vector2.zero;
        animator.SetBool("isRunning", false);
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
        Debug.Log("Facing the player!");
        transform.localScale = new Vector3(scaleX, transform.localScale.y, 1);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + box.center, box.size);
    }

}
