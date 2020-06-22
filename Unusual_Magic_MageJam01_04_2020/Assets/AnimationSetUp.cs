using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetUp : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    public void SetRunAnim(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }
}
