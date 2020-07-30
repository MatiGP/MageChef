using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSave", menuName ="Create New Save")]
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

    public List<Spice> listOfSpices;
    public List<int> listOfSpiceAmount;

    public void SetListOfSpices()
    {
        listOfSpices = new List<Spice>();

        foreach(Spice s in ownedSpices.Keys)
        {
            listOfSpices.Add(s);
        }
    }

    public void SetListOfSpiceAmount()
    {
        listOfSpiceAmount = new List<int>();

        foreach (Spice s in ownedSpices.Keys)
        {
            listOfSpiceAmount.Add(ownedSpices[s]);
        }
   
    }

    public void CreateDictionary()
    {
        Dictionary<Spice, int> tempDict = new Dictionary<Spice, int>();
        tempDict.Clear();
        int tempInt = 0;
        foreach (Spice s in listOfSpices)
        {
            tempDict.Add(s, listOfSpiceAmount[tempInt]);
            tempInt++;
        }

        ownedSpices = tempDict;
    }


}
