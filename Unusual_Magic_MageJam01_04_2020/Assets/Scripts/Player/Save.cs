using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSave", menuName ="Create New Save")]
[System.Serializable]
public class Save : ScriptableObject
{
    public int level;
    public int currentPoints = 0;
    public Recipe[] ownedRecipes = new Recipe[8];
    public Dictionary<Spice, int> ownedSpices = new Dictionary<Spice, int>();
    public int health = 4;
    public int maxHealth = 4;
    public Recipe[] craftedSpells = new Recipe[3];

    public Vector3? checkPoint;

    public List<Spice> listOfSpices = new List<Spice>();
    public List<int> listOfSpiceAmount = new List<int>();   

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

    public void SaveFile(int num)
    {
        SaveFile file = new SaveFile();
        file.level = level;
        file.currentPoints = currentPoints;
        file.ownedRecipes = ownedRecipes;
        file.health = health;
        file.maxHealth = maxHealth;
        file.craftedSpells = craftedSpells;
        file.listOfSpices = listOfSpices;
        file.listOfSpiceAmount = listOfSpiceAmount;

        string jsonString = JsonUtility.ToJson(file, true);
        File.WriteAllText(Application.persistentDataPath + "/Save" + num + ".json", jsonString);

       
    }

}

public class SaveFile
{
    public int level;
    public int currentPoints = 0;
    public Recipe[] ownedRecipes = new Recipe[8];
    public Dictionary<Spice, int> ownedSpices = new Dictionary<Spice, int>();
    public int health = 4;
    public int maxHealth = 4;
    public Recipe[] craftedSpells = new Recipe[3];

    public List<Spice> listOfSpices = new List<Spice>();
    public List<int> listOfSpiceAmount = new List<int>();
}
