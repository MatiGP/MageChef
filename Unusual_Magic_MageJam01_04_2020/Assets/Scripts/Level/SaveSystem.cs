using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    [SerializeField] List<Recipe> listOfRecipes;
    [SerializeField] List<Spice> listOfSpices;
    [SerializeField] GameObject player;

    [SerializeField] Save[] save;
    int currentSaveIndex;
    SaveFile currentSave;
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
        save[currentSaveIndex].SetListOfSpiceAmmount();

        save[currentSaveIndex].SaveFile(currentSaveIndex);       
    } 

    public void LoadState()
    {
        currentSave = new SaveFile();        
        currentSave = JsonUtility.FromJson<SaveFile>(File.ReadAllText(Application.persistentDataPath + "/Save" + currentSaveIndex + ".json"));

        save[currentSaveIndex].listOfSpices = GetSpicesFromID(currentSave.listOfSpiceIDs);
        save[currentSaveIndex].listOfSpiceAmount = currentSave.listOfSpiceAmount;
        save[currentSaveIndex].currentPoints = currentSave.currentPoints;
        save[currentSaveIndex].health = currentSave.health;

        save[currentSaveIndex].ownedRecipes = new Recipe[8];
        save[currentSaveIndex].ownedRecipes = GetRecipesFromID(currentSave.ownedRecipesIDs, 8);
        save[currentSaveIndex].craftedSpells = GetRecipesFromID(currentSave.craftedSpellIDs, 3);
        save[currentSaveIndex].CreateDictionary();
     
        save[currentSaveIndex].level = currentSave.level;
        save[currentSaveIndex].maxHealth = currentSave.maxHealth;
        
        if (currentSave.lastPositionX != 0 && currentSave.lastPositionY != 0)
        {
            save[currentSaveIndex].checkPoint = new Vector3(currentSave.lastPositionX, currentSave.lastPositionY, 0f);
        }
        else
        {
            save[currentSaveIndex].checkPoint = null;
        }

        
        
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

        if (save[currentSaveIndex].checkPoint.HasValue)
        {
            player.transform.position = (Vector3)save[currentSaveIndex].checkPoint;
            player.gameObject.SetActive(true);

        }
    }


    public void FinishLevel()
    {
        save[currentSaveIndex].level += 1;
        save[currentSaveIndex].checkPoint = null;
        save[currentSaveIndex].SaveFile(currentSaveIndex);
    }

    public void SaveCheckpoint(Vector3 pos)
    {
        save[currentSaveIndex].checkPoint = pos;
        save[currentSaveIndex].SaveFile(currentSaveIndex);
    }

    public void ResetCheckpoint()
    {
        save[currentSaveIndex].checkPoint = null;
        save[currentSaveIndex].SaveFile(currentSaveIndex);
    }

    Recipe[] GetRecipesFromID(List<int> recipeIDs, int requiredLen)
    {
        Recipe[] tempRecipeArray = new Recipe[requiredLen];
        int tempIndex = 0;

        foreach(Recipe r in listOfRecipes)
        {
            if (recipeIDs.Contains(r.ID))
            {
                tempRecipeArray[tempIndex] = r;
            }
        }

        return tempRecipeArray;
    }

    List<Spice> GetSpicesFromID(List<int> spiceIDs)
    {
        List<Spice> tempSpiceList = new List<Spice>();

        foreach(Spice s in listOfSpices)
        {
            if (spiceIDs.Contains(s.ID))
            {
                tempSpiceList.Add(s);
            }
        }

        return tempSpiceList;
    }
    
}
