using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float baseCooldown;
    public float abilityRange;

    public abstract void UseAbility();
    public abstract void SetUpAbilities();
}
