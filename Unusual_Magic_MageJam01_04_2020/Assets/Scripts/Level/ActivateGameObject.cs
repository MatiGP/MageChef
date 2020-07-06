using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    [SerializeField] string reactTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == reactTag)
        {
            objectToActivate.SetActive(true);
        }
    }
}
