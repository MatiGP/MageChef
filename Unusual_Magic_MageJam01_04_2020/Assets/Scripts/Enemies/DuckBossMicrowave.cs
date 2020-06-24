using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBossMicrowave : Attack
{
    [SerializeField] GameObject microwaveSprite;
    [SerializeField] GameObject cannons;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] movePoints;
    [SerializeField] float microwaveSpeed;
    [SerializeField] float stayTimeOnPoint;
    [SerializeField] Animator animator;
    [SerializeField] BossAI boss;
    [SerializeField] Transform shootPoint;
    [SerializeField] AudioSource laserSound;
    bool arrived;
    int destinationIndex;

    float currentAttackCD = 0f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5);
        microwaveSprite.SetActive(true);
        cannons.SetActive(false);
        destinationIndex = Random.Range(0, movePoints.Length);
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, movePoints[destinationIndex].position) < 1f && !arrived)
        {
            arrived = true;
            StartCoroutine(SelectNewDestination());
        }
        Move(destinationIndex);

        if(currentAttackCD >= 0f)
        {
            currentAttackCD -= Time.deltaTime;
        }
    }

    IEnumerator SelectNewDestination()
    {
        yield return new WaitForSeconds(stayTimeOnPoint);
        destinationIndex = Random.Range(0, movePoints.Length);
        arrived = false;
    }

    

    private void Move(int destinationIndex)
    {
       transform.position = Vector2.MoveTowards(transform.position, movePoints[destinationIndex].position, microwaveSpeed * Time.deltaTime);
    }



    public override void DoAttack()
    {
        if(currentAttackCD <= 0f && arrived)
        {
            animator.SetTrigger("attack");
            GameObject go = Instantiate(projectile, shootPoint.position, Quaternion.identity);
            go.GetComponent<DuckBossLaserProjectile>().SetTarget(boss.GetTarget());
            currentAttackCD = attackCooldown;
            laserSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage();
        }
    }
}
