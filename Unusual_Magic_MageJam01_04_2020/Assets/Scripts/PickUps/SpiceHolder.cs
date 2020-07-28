using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceHolder : MonoBehaviour
{

    public Spice spice;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = spice.spiceIcon;
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

    public void SetUpSpice(Spice spice)
    {
        sr.sprite = spice.spiceIcon;
        this.spice = spice;
    }
}
