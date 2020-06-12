using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBossMicrowave : Attack
{
    [SerializeField] Transform[] movePoints;
    [SerializeField] float microwaveSpeed;
    [SerializeField] float stayTimeOnPoint;
    [SerializeField] Animator animator;
    [SerializeField] LineRenderer lineRenderer;
    bool arrived;
    int destinationIndex;



    private void Start()
    {
        destinationIndex = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        if(transform.position == movePoints[destinationIndex].position)
        {           
            StartCoroutine(SelectNewDestination());
        }
        Move(destinationIndex);
    }

    IEnumerator SelectNewDestination()
    {
        yield return new WaitForSeconds(stayTimeOnPoint);
        destinationIndex = Random.Range(0, movePoints.Length);      
    }

    IEnumerator ShootLaser()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1f);
        
    }

    private void Move(int destinationIndex)
    {
       transform.position = Vector2.MoveTowards(transform.position, movePoints[destinationIndex].position, microwaveSpeed * Time.deltaTime);
    }



    public override void DoAttack()
    {
        StartCoroutine
    }
}
