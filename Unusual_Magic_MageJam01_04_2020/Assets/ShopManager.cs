using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Spice[] spices;
    [SerializeField] Recipe[] recipes;
    [SerializeField] MaxHealthPickUp hpPickup;
    [SerializeField] RangedInt lifePrice;
    [SerializeField] RangedInt recipePrice;
    [SerializeField] RangedInt spicePrice;
    [SerializeField] ShopItem[] shopItems;

    enum ShopCategory { Life, Recipe, Spice }

    private void Start()
    {
        foreach (ShopItem si in shopItems)
        {
            ShopCategory randomCategory = (ShopCategory)Random.Range(0, 3);

            switch (randomCategory)
            {
                case ShopCategory.Life:
                    si.SetShopItem(lifePrice.GetRandomValue(), hpPickup, hpPickup.GetIcon());
                    break;

                case ShopCategory.Recipe:
                    int randomRecipe = Random.Range(0, recipes.Length);
                    si.SetShopItem(recipePrice.GetRandomValue(), recipes[randomRecipe], recipes[randomRecipe].GetIcon());
                    break;

                case ShopCategory.Spice:
                    int randomSpice = Random.Range(0, spices.Length);
                    si.SetShopItem(spicePrice.GetRandomValue(), spices[randomSpice], spices[randomSpice].GetIcon());
                    break;
            }
        }
        
    }
}
[System.Serializable]
public class RangedInt
{
    public int min;
    public int max;

    public int GetRandomValue()
    {
        return Random.Range(min, max);
    }
}
