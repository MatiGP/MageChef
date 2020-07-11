using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookieDropKick : Attack
{
    [SerializeField] float dropKickSpeed = 8f;
    [SerializeField] float dropKickTime = 1.5f;
    float currentDropKickTime = 0f;
    WrestlingCookieAI cookieAI;
    Transform nextPos;
    bool dKick = false;
    bool canAttack = true;
    bool lockDirection = true;
    float xLocalScale;
    float currentCooldown;
    Vector2 colliderSize;

    public override void DoAttack()
    {
        if (canAttack)
        {
            Debug.Log("DROP");
            currentDropKickTime = dropKickTime;
            xLocalScale = transform.localScale.x;
        }
       
    }

    private void Start()
    {
        cookieAI = GetComponent<WrestlingCookieAI>();
        colliderSize = GetComponent<CapsuleCollider2D>().size;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDropKickTime <= 0f)
        {
            currentCooldown = attackCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
            canAttack = false;
            GetComponent<CapsuleCollider2D>().size = colliderSize;
            if (currentCooldown <= 0f)
            {
                canAttack = true;
            }
        }

        if (canAttack && currentDropKickTime > 0f && currentCooldown <= 0f)
        {

            cookieAI.TriggerAnimationAttack1();
            cookieAI.Move(dropKickSpeed * xLocalScale);
            cookieAI.FreezePositionY();
            GetComponent<CapsuleCollider2D>().size = new Vector2(1.25f, 1.78f);
            currentDropKickTime -= Time.deltaTime;
        }
    }

  
}
