using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public Recipe[] unlockedRecipes = new Recipe[8];
    Dictionary<Spice, int> ownedSpices = new Dictionary<Spice, int>();

    public bool HasSpice(Spice spice)
    {
        Debug.Log("Checking . . .");
        return ownedSpices.ContainsKey(spice);
    }

    public void AddNewSpice(Spice spice)
    {
        Debug.Log("I found " + spice.spiceName);
        ownedSpices.Add(spice, 1);
    }

    public void AddSpice(Spice spice)
    {
        Debug.Log("I found " + spice.spiceName + " before I had " + ownedSpices[spice]);
        ownedSpices[spice]++;
        Debug.Log("Now, I have " + ownedSpices[spice] + " of " + spice.spiceName);
    }
}
