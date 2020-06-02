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
        LoadState();
    }

    public void SaveState()
    {
        save.currentPoints = player.GetComponent<PlayerPoints>().GetPoints();
        save.health = player.GetComponent<Health>().GetHealthAmmount();
        save.ownedRecipes = player.GetComponent<PlayerAbilities>().unlockedRecipes;
        save.ownedSpices = player.GetComponent<PlayerAbilities>().GetOwnedSpices();
    }

    public void LoadState()
    {
        player.GetComponent<PlayerPoints>().AddPoints(save.currentPoints);
        player.GetComponent<Health>().SetHealth(save.health);
        
        foreach(Recipe r in save.ownedRecipes)
        {
            player.GetComponent<PlayerAbilities>().AddNewRecipe(r);
        }

        player.GetComponent<PlayerAbilities>().SetDict(save.ownedSpices);
    }
}
