using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpellIcons : MonoBehaviour
{
    [SerializeField]PlayerAbilities pa;
    [SerializeField] Image[] spellIcons = new Image[3];

    private void Start()
    {
        pa.OnSpellCrafted += Pa_OnSpellCrafted;
    }

    private void Pa_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellIcons[(int)e.recipe.slot].gameObject.SetActive(true);
        spellIcons[(int)e.recipe.slot].sprite = e.recipe.recipeIcon;
    }
}
