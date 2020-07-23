using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "New Recipe")]
public class Recipe : ScriptableObject, ISellable
{
    public Sprite recipeIcon;
    public Sprite spellbookRecipeIcon;
    public Ingredient[] ingredients = new Ingredient[3];
    public GameObject result;
    public float cooldown;

    public enum SpellSlot { Main, Secondary, Tertiary }
    public SpellSlot slot;

    public void Sell(GameObject player)
    {
        player.GetComponent<PlayerAbilities>().AddNewRecipe(this);
    }
}
[System.Serializable]
public class Ingredient
{
    public Spice requiredSpice;

    public int requiredAmount;
}

