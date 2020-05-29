using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeHolder : MonoBehaviour
{
    public Recipe recipe;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = recipe.recipeIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerAbilities pa = collision.GetComponent<PlayerAbilities>();

            pa.AddNewRecipe(recipe);

            Destroy(gameObject);
        }
    }
}
