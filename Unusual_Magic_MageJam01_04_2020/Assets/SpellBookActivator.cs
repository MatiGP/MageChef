using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookActivator : MonoBehaviour
{
    [SerializeField] GameObject spellBook;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spellBook.SetActive(true);
        Destroy(gameObject);
    }
}
