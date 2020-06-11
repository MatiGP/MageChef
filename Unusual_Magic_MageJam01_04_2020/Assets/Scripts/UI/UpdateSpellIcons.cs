using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpellIcons : MonoBehaviour
{
    [SerializeField] PlayerAbilities pa;
    [SerializeField] Image[] spellIcons = new Image[3];
    [SerializeField] PlayerAbilitiesManager pam;

    private void Start()
    {
        pa.OnSpellCrafted += Pa_OnSpellCrafted;
        Recipe[] rec = pam.GetOwnedSpells();
        for(int i = 0; i < 3; i++)
        {
            if (rec[i] == null)
            {
                spellIcons[i].gameObject.SetActive(false);
            }
            else
            {
                spellIcons[(int)rec[i].slot].gameObject.SetActive(true);
                spellIcons[(int)rec[i].slot].sprite = rec[i].spellbookRecipeIcon;
            }
        }

    }

    private void Pa_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellIcons[(int)e.recipe.slot].gameObject.SetActive(true);
        spellIcons[(int)e.recipe.slot].sprite = e.recipe.spellbookRecipeIcon;
    }
}
