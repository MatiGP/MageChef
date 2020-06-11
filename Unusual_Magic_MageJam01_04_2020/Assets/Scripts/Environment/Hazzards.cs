﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzards : MonoBehaviour
{
    [SerializeField] int damage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage();
        }
    }
}