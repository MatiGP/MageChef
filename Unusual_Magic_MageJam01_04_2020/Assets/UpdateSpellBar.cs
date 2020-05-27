using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpellBar : MonoBehaviour
{
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] Image[] spellImages = new Image[3];

    private void Start()
    {
        playerAbilities.OnSpellCrafted += PlayerAbilities_OnSpellCrafted;
    }

    private void PlayerAbilities_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellImages[(int)e.recipe.slot].gameObject.SetActive(true);
        spellImages[(int)e.recipe.slot].sprite = e.recipe.recipeIcon;
    }
}
