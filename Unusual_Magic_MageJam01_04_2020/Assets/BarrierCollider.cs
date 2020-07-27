using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IProjectile projectile = collision.GetComponent<IProjectile>();

        if(projectile != null)
        {
            projectile.DestroyProjectile();
        }
    }
}
