using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour
{
    [SerializeField] LayerMask destructableLayer;
    [SerializeField] float destructionRadius;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destructable")
        {
            Tilemap tilemap = collision.GetComponent<Tilemap>();

            Vector3Int tilePos = tilemap.WorldToCell(transform.position);
            tilePos.x += 1 * (int)transform.localScale.x;
            tilemap.SetTile(tilePos, null);
            Destroy(gameObject);
        }

        
    }

    
}
