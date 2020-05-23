using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] spellGameObjects = new GameObject[3];
    [SerializeField] Transform castPlace;
    PlayerAbilities playerAbilities;

    // Start is called before the first frame update
    void Start()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        playerAbilities.OnSpellCrafted += PlayerAbilities_OnSpellCrafted;
    }

    private void PlayerAbilities_OnSpellCrafted(object sender, PlayerAbilities.OnSpellCraftedArgs e)
    {
        spellGameObjects[0] = e.recipe.result;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (spellGameObjects[0] != null)
            {
                Instantiate(spellGameObjects[0], castPlace.position, Quaternion.identity);
            }
        }
    }
}
