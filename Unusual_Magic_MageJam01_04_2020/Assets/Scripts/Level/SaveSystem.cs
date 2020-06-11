using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SaveState();
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

        if(save.checkPoint.HasValue){
            player.transform.position = (Vector3)save.checkPoint;
            player.gameObject.SetActive(true);
        }else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    public void SaveCheckpoint(Vector3 pos)
    {
        save.checkPoint = pos;
    }

    public void ResetCheckpoint()
    {
        save.checkPoint = null;
    }
}
