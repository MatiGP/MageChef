using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSave", menuName ="Create New Save")]
public class Save : ScriptableObject
{
    public int currentPoints;
    public Recipe[] ownedRecipes;
    public Dictionary<Spice, int> ownedSpices;
    public int health;   
}
