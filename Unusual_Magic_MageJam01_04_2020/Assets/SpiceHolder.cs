using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceHolder : MonoBehaviour
{
    public Spice spice;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spice.spiceIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerAbilities pa = collision.GetComponent<PlayerAbilities>();

            if (pa.HasSpice(spice))
            {
                pa.AddSpice(spice);
            }
            else
            {
                pa.AddNewSpice(spice);
            }

            Destroy(gameObject);
        }
    }
}
