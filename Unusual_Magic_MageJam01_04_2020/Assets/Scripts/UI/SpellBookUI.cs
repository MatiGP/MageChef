using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookUI : MonoBehaviour
{
    [SerializeField] GameObject spellBook;
    [SerializeField] GameObject recipeTab;
    [SerializeField] GameObject spiceTab;
    [SerializeField] Color32 disableColor;
    [SerializeField] Color32 enableColor;
    [SerializeField] Image recipeTabIcon;
    [SerializeField] Image spiceTabIcon;
    bool isSpellBookOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        recipeTab.SetActive(true);
        spiceTab.SetActive(false);
        recipeTabIcon.color = enableColor;
        spiceTabIcon.color = disableColor;
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

        if (isSpellBookOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                recipeTab.SetActive(true);
                spiceTab.SetActive(false);
                recipeTabIcon.color = enableColor;
                spiceTabIcon.color = disableColor;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)){
                recipeTab.SetActive(false);
                spiceTab.SetActive(true);
                recipeTabIcon.color = disableColor;
                spiceTabIcon.color = enableColor;
            }
        }
    }
}
