using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] GameObject connectedDoor;    
    [SerializeField] string reactTag;
    [SerializeField] Animator animator;
    [SerializeField] bool timedOpen;   
    [SerializeField] float openTime;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == reactTag)
        {
            connectedDoor.SetActive(false);
            animator.SetTrigger("buttonPressed");
            if (timedOpen)
            {
                StartCoroutine(EnableDoor());
            }                    
        }
    }

    IEnumerator EnableDoor()
    {
        yield return new WaitForSeconds(openTime);
        connectedDoor.SetActive(true);
    }
}
