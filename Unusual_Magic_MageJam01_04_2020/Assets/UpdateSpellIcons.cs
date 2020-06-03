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
        foreach(Recipe rec in pam.GetOwnedSpells())
        {
            if (rec == null)
            {
                spellIcons[(int)rec.slot].gameObject.SetActive(false);
            }
            else
            {
                spellIcons[(int)rec.slot].gameObject.SetActive(true);
                spellIcons[(int)rec.slot].sprite = rec.spellbookRecipeIcon;
            }
        }
    }

    private void Pa_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellIcons[(int)e.recipe.slot].gameObject.SetActive(true);
        spellIcons[(int)e.recipe.slot].sprite = e.recipe.spellbookRecipeIcon;
    }
}
