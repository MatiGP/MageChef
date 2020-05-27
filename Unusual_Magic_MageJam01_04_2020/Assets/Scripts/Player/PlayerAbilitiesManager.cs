using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManager : MonoBehaviour
{
    [SerializeField] Recipe[] spellGameObjects = new Recipe[3];
    [SerializeField] float[] spellCooldowns = new float[3];
    [SerializeField] Transform castPlace;
    PlayerAbilities playerAbilities;

    float currentCooldownSpell1 = 0f;
    float currentCooldownSpell2 = 0f;
    float currentCooldownSpell3 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerAbilities.OnSpellCrafted += PlayerAbilities_OnSpellCrafted;
        playerAbilities.OnSpellCrafted += UpdateSpellCooldowns;
    }

    private void PlayerAbilities_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellGameObjects[(int)e.recipe.slot] = e.recipe;
    }

    private void UpdateSpellCooldowns(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellCooldowns[(int)e.recipe.slot] = e.recipe.spellCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentCooldownSpell1 <= 0f)
        {
            if (spellGameObjects[0] != null)
            {
                GameObject go = Instantiate(spellGameObjects[0].result, castPlace.position, transform.rotation);
                go.transform.localScale = new Vector3(go.transform.localScale.x * transform.localScale.x, go.transform.localScale.y , 1);
                currentCooldownSpell1 = spellCooldowns[0];
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (spellGameObjects[1] != null)
            {
                Instantiate(spellGameObjects[1].result, castPlace.position, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (spellGameObjects[2] != null)
            {
                Instantiate(spellGameObjects[2].result, castPlace.position, Quaternion.identity);
            }
        }
        
        currentCooldownSpell1 -= Time.deltaTime;

    }
}
