using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookUI : MonoBehaviour
{
    [SerializeField] GameObject spellBook;
    bool isSpellBookOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSpellBookOpen)
        {
            spellBook.SetActive(true);
            isSpellBookOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.Q) && isSpellBookOpen)
        {
            spellBook.SetActive(false);
            isSpellBookOpen = false;

        }
    }
}
