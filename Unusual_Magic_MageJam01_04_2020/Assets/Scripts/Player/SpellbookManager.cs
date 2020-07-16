using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookManager : MonoBehaviour
{
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] SpellbookRecipe[] spellbookRecipes;
    [SerializeField] SpellbookSpice[] spellbookSpices;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAbilities.OnRecipeCollected += PlayerAbilities_OnRecipeCollected;
        RefreshRecipesInSpellBook();

        playerAbilities.OnSpicePicked += PlayerAbilities_OnSpicePicked;
        RefreshSpicesInSpellBook();
    }

    private void PlayerAbilities_OnSpicePicked(object sender, PlayerAbilities.OnSpicePickedUp e)
    {
        RefreshSpicesInSpellBook();
    }

    private void RefreshSpicesInSpellBook()
    {
        Dictionary<Spice, int> ownedSpices = playerAbilities.GetOwnedSpices();
        int i = 0;

        foreach (Spice s in ownedSpices.Keys)
        {
            spellbookSpices[i].gameObject.SetActive(true);
            spellbookSpices[i].SetUpSpice(s);
            i++;
        }
        for (int j = i; j < spellbookSpices.Length; j++)
        {
            spellbookSpices[i].gameObject.SetActive(false);
        }
    }

    private void RefreshRecipesInSpellBook()
    {
        for (int i = 0; i < spellbookRecipes.Length; i++)
        {
            if (playerAbilities.unlockedRecipes[i] == null)
            {
                spellbookRecipes[i].gameObject.SetActive(false);
            }
            else
            {
                spellbookRecipes[i].gameObject.SetActive(true);
                spellbookRecipes[i].SetUpRecipe(playerAbilities.unlockedRecipes[i]);
            }

        }
    }

    private void PlayerAbilities_OnRecipeCollected(object sender, PlayerAbilities.OnRecipeCollectedArgs e)
    {
        RefreshRecipesInSpellBook();
    }
}
