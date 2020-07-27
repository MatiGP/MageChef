using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellbookRecipe : MonoBehaviour
{
    [SerializeField] Image[] recipeElements;
    [SerializeField] TextMeshProUGUI[] spiceRequiredAmount;

    public void SetUpRecipe(Recipe reci)
    {
        GetComponent<Image>().sprite = reci.spellbookRecipeIcon;
        for(int i = 0; i < 3; i++)
        {
            recipeElements[i].sprite = reci.ingredients[i].requiredSpice.spiceIcon;
            spiceRequiredAmount[i].text = reci.ingredients[i].requiredAmount.ToString();
        }
    }
}
