using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadSavesFromFiles : MonoBehaviour
{
    [SerializeField] Save[] saves;

    // Start is called before the first frame update
    void Awake()
    {

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/Save" + i + ".json"))
            {
                Save currentSave = new Save();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/Save" + i + ".json"), currentSave);
                print(currentSave.listOfSpices[0]);
                currentSave.SetListOfSpiceAmount();
                currentSave.SetListOfSpices();
                
                currentSave.CreateDictionary();
                
                saves[i].currentPoints = currentSave.currentPoints;
                saves[i].health = currentSave.health;

                saves[i].ownedRecipes = new Recipe[8];
                saves[i].ownedRecipes = currentSave.ownedRecipes;
                saves[i].listOfSpices = currentSave.listOfSpices;
                saves[i].listOfSpiceAmount = currentSave.listOfSpiceAmount;
                saves[i].CreateDictionary();
                saves[i].ownedSpices = currentSave.ownedSpices;
                saves[i].craftedSpells = currentSave.craftedSpells;
                saves[i].level = currentSave.level;
                saves[i].maxHealth = currentSave.maxHealth;
                
            }
        }
    }

  
}
