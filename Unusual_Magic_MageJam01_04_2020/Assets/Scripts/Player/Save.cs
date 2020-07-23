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
}
