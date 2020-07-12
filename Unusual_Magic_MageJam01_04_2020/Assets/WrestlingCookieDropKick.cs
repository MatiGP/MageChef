using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookieDropKick : Attack
{
    [SerializeField] float dropKickSpeed = 8f;
    [SerializeField] float dropKickTime = 1.5f;
    [SerializeField] GameObject dropKickCollider;
    WrestlingCookieAI cookieAI;

    bool canAttack = true;

    float xLocalScale;
    float currentCooldown;
    float currentDropKickTime = 1.5f;

    Vector2 colliderSize;

    public override void DoAttack()
    {
        if (currentCooldown <= 0f)
        {
            Debug.Log("DROP");
            currentDropKickTime = dropKickTime;
            xLocalScale = transform.localScale.x;
            GetComponent<CapsuleCollider2D>().size = new Vector2(1.25f, 1.78f);
        }        
       
    }

    private void Awake()
    {
        cookieAI = GetComponent<WrestlingCookieAI>();
        colliderSize = GetComponent<CapsuleCollider2D>().size;
    }
    // Vector2(1.25f, 1.78f)
    // Update is called once per frame

    void Update()
    {
        if(currentDropKickTime > 0f)
        {
            cookieAI.Move(dropKickSpeed * xLocalScale);
            cookieAI.FreezePositionY();
            cookieAI.TriggerAnimationAttack1();
            currentDropKickTime -= Time.deltaTime;
            dropKickCollider.SetActive(true);
            currentCooldown = attackCooldown;
        }
        else
        {
            cookieAI.UnFreezePositionY();
            GetComponent<CapsuleCollider2D>().size = colliderSize;
            dropKickCollider.SetActive(false);
        }

        currentCooldown -= Time.deltaTime;
    }

  
}
