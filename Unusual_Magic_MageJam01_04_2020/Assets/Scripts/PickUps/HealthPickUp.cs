using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] AudioSource hpPickUpSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.GetComponent<Health>().GetHealthAmmount() < 4)
            {
                collision.GetComponent<Health>().RestoreHealth();
                hpPickUpSource.Play();
                StartCoroutine(Disable());
            }
            
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
