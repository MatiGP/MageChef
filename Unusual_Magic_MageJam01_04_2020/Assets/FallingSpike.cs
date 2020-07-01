using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] Rigidbody2D spike;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Collider2D>().enabled = false;
            Fall();
        }
    }

    public void Fall()
    {
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    {
        spike.GetComponent<PlatformDestroyer>().enabled = true;
        yield return new WaitForSeconds(delay);
        spike.gravityScale = 1.6f;
    }


}
