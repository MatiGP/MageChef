using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    [SerializeField] GameObject player;

    [SerializeField] Save[] save;
    int currentSaveIndex;

    private void Awake()
    {
        instance = this;
        currentSaveIndex = PlayerPrefs.GetInt("selectedLevel");
        LoadState();
    }

    public void SaveState()
    {
        save[currentSaveIndex].currentPoints = player.GetComponent<PlayerPoints>().GetPoints();
        save[currentSaveIndex].health = player.GetComponent<Health>().GetHealthAmmount();

        save[currentSaveIndex].ownedRecipes = new Recipe[8];
        save[currentSaveIndex].ownedRecipes = player.GetComponent<PlayerAbilities>().unlockedRecipes;

        save[currentSaveIndex].ownedSpices = player.GetComponent<PlayerAbilities>().GetOwnedSpices();
        save[currentSaveIndex].craftedSpells = player.GetComponent<PlayerAbilitiesManager>().GetOwnedSpells();
        save[currentSaveIndex].level = SceneManager.GetActiveScene().buildIndex;
        save[currentSaveIndex].maxHealth = player.GetComponent<Health>().GetMaxHealth();
        save[currentSaveIndex].SetListOfSpices();
        save[currentSaveIndex].SetListOfSpiceAmount();

        string jsonString = JsonUtility.ToJson(save[currentSaveIndex], true);
        File.WriteAllText(Application.persistentDataPath + "/Save" + currentSaveIndex + ".json", jsonString);
    } 

    public void LoadState()
    {
        Save currentSave = new Save();
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/Save" + currentSaveIndex + ".json"), currentSave);

        save[currentSaveIndex].listOfSpiceAmount = currentSave.listOfSpiceAmount;
        save[currentSaveIndex].listOfSpices = currentSave.listOfSpices;
        save[currentSaveIndex].CreateDictionary();
        save[currentSaveIndex].currentPoints = currentSave.currentPoints;
        save[currentSaveIndex].health = currentSave.health;

        save[currentSaveIndex].ownedRecipes = new Recipe[8];
        save[currentSaveIndex].ownedRecipes = currentSave.ownedRecipes;

        save[currentSaveIndex].ownedSpices = currentSave.ownedSpices;
        save[currentSaveIndex].craftedSpells = currentSave.craftedSpells;
        save[currentSaveIndex].level = currentSave.level;
        save[currentSaveIndex].maxHealth = currentSave.maxHealth;


        player.gameObject.transform.parent = null;
        player.GetComponent<PlayerAbilities>().ResetRecipeCounter();
        player.GetComponent<PlayerPoints>().ResetPoints();
        player.GetComponent<PlayerPoints>().AddPoints(save[currentSaveIndex].currentPoints);
        player.GetComponent<Health>().SetMaxHP(save[currentSaveIndex].maxHealth);
        player.GetComponent<Health>().SetHealth(save[currentSaveIndex].health);
        player.GetComponent<PlayerAbilities>().SetDict(save[currentSaveIndex].ownedSpices);       
        player.GetComponent<PlayerAbilitiesManager>().SetSpells(save[currentSaveIndex].craftedSpells);



        foreach (Recipe r in save[currentSaveIndex].ownedRecipes)
        {
            if (r != null)
            {
                player.GetComponent<PlayerAbilities>().AddNewRecipe(r);
            }          
            
        }

        player.GetComponentInChildren<UpdateSpellIcons>()?.SetSpellIcons();
        

    }

    public void LoadCheckpoint()
    {
        LoadState();

        if (save[currentSaveIndex].checkPoint.HasValue)
        {
            player.transform.position = (Vector3)save[currentSaveIndex].checkPoint;
            player.gameObject.SetActive(true);
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SaveCheckpoint(Vector3 pos)
    {
        save[currentSaveIndex].checkPoint = pos;
    }

    public void ResetCheckpoint()
    {
        save[currentSaveIndex].checkPoint = null;
    }
}
