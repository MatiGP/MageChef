using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    [SerializeField] GameObject player;

    [SerializeField] Save save;

    private void Awake()
    {
        instance = this;
       
    }

    private void Start()
    {
        LoadState();
    }

    public void SaveState()
    {
        save.currentPoints = player.GetComponent<PlayerPoints>().GetPoints();
        save.health = player.GetComponent<Health>().GetHealthAmmount();
        save.ownedRecipes = new Recipe[8];
        save.ownedRecipes = player.GetComponent<PlayerAbilities>().unlockedRecipes;
        save.ownedSpices = player.GetComponent<PlayerAbilities>().GetOwnedSpices();
        save.craftedSpells = player.GetComponent<PlayerAbilitiesManager>().GetOwnedSpells();
    } 
    public void LoadState()
    {
        player.GetComponent<PlayerPoints>().AddPoints(save.currentPoints);
        player.GetComponent<Health>().SetHealth(save.health);
        player.GetComponent<PlayerAbilities>().SetDict(save.ownedSpices);
        player.GetComponent<PlayerAbilitiesManager>().SetSpells(save.craftedSpells);

        foreach (Recipe r in save.ownedRecipes)
        {
            if (r == null) continue;            
            player.GetComponent<PlayerAbilities>().AddNewRecipe(r);
        }
        
    }
}
