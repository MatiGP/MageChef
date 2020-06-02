using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainReseter : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] rbs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StopCoroutine(Reset());
        }
        SetAngularDrag(0.05f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(4f);
        SetAngularDrag(2000f);
    }

    public void SetAngularDrag(float ang)
    {
        foreach(Rigidbody2D rb in rbs)
        {
            rb.angularDrag = ang;
        }
    }
}
