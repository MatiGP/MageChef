using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManager : MonoBehaviour
{
    [SerializeField] Recipe[] spellGameObjects = new Recipe[3];
    [SerializeField] Transform castPlace;
    PlayerAbilities playerAbilities;

    float cooldownSpell1;
    float cooldownSpell2;
    float cooldownSpell3;

    float currentCooldownSpell1;
    float currentCooldownSpell2;
    float currentCooldownSpell3;

    // Start is called before the first frame update
    void Start()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerAbilities.OnSpellCrafted += PlayerAbilities_OnSpellCrafted;

        if (spellGameObjects[0] != null)
        {
            cooldownSpell1 = spellGameObjects[0].cooldown;
        }
        if (spellGameObjects[1] != null)
        {
            cooldownSpell2 = spellGameObjects[1].cooldown;
        }
        if (spellGameObjects[2] != null)
        {
            cooldownSpell3 = spellGameObjects[2].cooldown;
        }       
    }

    private void PlayerAbilities_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellGameObjects[(int)e.recipe.slot] = e.recipe;
        cooldownSpell1 = e.recipe.cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCooldownSpell1 <= 0)
        {
            if (spellGameObjects[0] != null)
            {
                GameObject go = Instantiate(spellGameObjects[0].result, castPlace.position, Quaternion.identity);
                go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, 1);
                currentCooldownSpell1 = cooldownSpell1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && currentCooldownSpell2 <= 0)
        {
            if (spellGameObjects[1] != null )
            {
                Instantiate(spellGameObjects[1].result, castPlace.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && currentCooldownSpell3 <= 0)
        {
            if (spellGameObjects[2] != null)
            {
                Instantiate(spellGameObjects[2].result, castPlace.position, Quaternion.identity);
                currentCooldownSpell3 = cooldownSpell3;
            }
        }

        currentCooldownSpell1 -= Time.deltaTime;
        //currentCooldownSpell2 -= Time.deltaTime;
        currentCooldownSpell3 -= Time.deltaTime;
    }

    public Recipe[] GetOwnedSpells()
    {
        return spellGameObjects;
    }

    public void SetSpells(Recipe[] spells)
    {
        spellGameObjects = spells;
    }
}
