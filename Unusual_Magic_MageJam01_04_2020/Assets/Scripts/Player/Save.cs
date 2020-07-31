using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSave", menuName ="Create New Save")]
public class Save : ScriptableObject
{
    public int level;
    public int currentPoints = 0;
    public int health = 4;
    public int maxHealth = 4;
    public Recipe[] ownedRecipes = new Recipe[8];
    public Recipe[] craftedSpells = new Recipe[3];

    public Dictionary<Spice, int> ownedSpices = new Dictionary<Spice, int>();

    public List<Spice> listOfSpices;
    public List<int> listOfSpiceAmount;

    public Vector3? checkPoint;  

    public void CreateDictionary()
    {
        Dictionary<Spice, int> tempDict = new Dictionary<Spice, int>();

        int tempInt = 0;
        foreach (Spice s in listOfSpices)
        {
            tempDict.Add(s, listOfSpiceAmount[tempInt]);
            tempInt++;
        }

        ownedSpices = tempDict;
    }

    public void SetListOfSpiceAmmount()
    {
        listOfSpiceAmount = new List<int>();

        foreach(Spice s in ownedSpices.Keys)
        {
            listOfSpiceAmount.Add(ownedSpices[s]);
        }
    }

    public void ResetSave()
    {
        level = 0;
        currentPoints = 0;
        health = 4;
        maxHealth = 4;
        ownedRecipes = new Recipe[8];
        craftedSpells = new Recipe[3];

        ownedSpices = new Dictionary<Spice, int>();

        listOfSpices = new List<Spice>();
        listOfSpiceAmount = new List<int>();
    }

    public void SaveFile(int num)
    {
        SaveFile file = new SaveFile();
        file.level = level;
        file.currentPoints = currentPoints;

        file.craftedSpellIDs = new List<int>();
        foreach(Recipe r in craftedSpells)
        {
            if(r != null)
            {
                file.craftedSpellIDs.Add(r.ID);
            }
            
        }

        file.ownedRecipesIDs = new List<int>();
        foreach(Recipe r in ownedRecipes)
        {
            if(r != null)
            {
                file.ownedRecipesIDs.Add(r.ID);
            }           
        }

        file.listOfSpiceIDs = new List<int>();
        foreach(Spice s in ownedSpices.Keys)
        {
            if(s != null)
            {
                file.listOfSpiceIDs.Add(s.ID);
            }          
        }

        file.health = health;
        file.maxHealth = maxHealth;       
        file.listOfSpiceAmount = listOfSpiceAmount;

        string jsonString = JsonUtility.ToJson(file, true);
        File.WriteAllText(Application.persistentDataPath + "/Save" + num + ".json", jsonString);

       
    }

}
[System.Serializable]
public class SaveFile
{
    public int level;
    public int currentPoints = 0;
    public List<int> ownedRecipesIDs;
    public int health = 4;
    public int maxHealth = 4;
    public List<int> craftedSpellIDs;
    public List<int> listOfSpiceIDs;
    public List<int> listOfSpiceAmount;
}
