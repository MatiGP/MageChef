using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpikes : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(TriggerSpikes());       
    }

    IEnumerator TriggerSpikes()
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("setSpikes");
    }


}
