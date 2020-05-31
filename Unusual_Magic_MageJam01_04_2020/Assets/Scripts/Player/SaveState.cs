using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : ScriptableObject
{
    public int collectedPoints;
    public Spice[] collectedSpices;
    public Recipe[] collectedRecipes;
}
