using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookManager : MonoBehaviour
{
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] SpellbookRecipe[] spellbookRecipes;
    // Start is called before the first frame update
    void Start()
    {
        playerAbilities.OnRecipeCollected += PlayerAbilities_OnRecipeCollected;
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
}
