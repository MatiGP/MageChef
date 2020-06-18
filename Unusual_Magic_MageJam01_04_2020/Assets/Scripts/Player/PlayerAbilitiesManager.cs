using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManager : MonoBehaviour
{
    [SerializeField] Recipe[] spellGameObjects = new Recipe[3];
    [SerializeField] Transform castPlace;
    PlayerAbilities playerAbilities;

    float[] spellCooldowns = new float[3];

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
            spellCooldowns[0] = spellGameObjects[0].cooldown;
        }
        if (spellGameObjects[1] != null)
        {
            spellCooldowns[1] = spellGameObjects[1].cooldown;
        }
        if (spellGameObjects[2] != null)
        {
            spellCooldowns[2] = spellGameObjects[2].cooldown;
        }       
    }

    private void PlayerAbilities_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellGameObjects[(int)e.recipe.slot] = e.recipe;
        spellCooldowns[(int)e.recipe.slot] = e.recipe.cooldown;
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
                currentCooldownSpell1 = spellCooldowns[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && currentCooldownSpell2 <= 0)
        {
            if (spellGameObjects[1] != null )
            {
                GameObject go = Instantiate(spellGameObjects[1].result, castPlace.position, Quaternion.identity);
                go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, 1);
                currentCooldownSpell2 = spellCooldowns[1];
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && currentCooldownSpell3 <= 0)
        {
            if (spellGameObjects[2] != null)
            {
                Instantiate(spellGameObjects[2].result, castPlace.position, Quaternion.identity);
                currentCooldownSpell3 = spellCooldowns[2];
            }
        }

        currentCooldownSpell1 -= Time.deltaTime;
        currentCooldownSpell2 -= Time.deltaTime;
        //currentCooldownSpell3 -= Time.deltaTime;
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
