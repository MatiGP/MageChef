using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public float attackCooldown;
    public float attackRange;

    public AudioSource onAttackAudio;
    public abstract void DoAttack();
}
