using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellcraftingUI : MonoBehaviour
{
    [SerializeField] GameObject[] arrows = new GameObject[3];
    [SerializeField] Image[] spiceImages = new Image[3];
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] Sprite blankSprite;
    int activeSlot;
    void Start()
    {
        playerAbilities.OnSpiceChanged += PlayerAbilities_OnSpiceChanged;
        playerAbilities.OnSpiceSlotChanged += PlayerAbilities_OnSpiceSlotChanged;
        arrows[activeSlot].gameObject.SetActive(true);
    }

    private void PlayerAbilities_OnSpiceSlotChanged(object sender, PlayerAbilities.OnSpiceSlotChangedArgs e)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == e.spiceSlot)
            {
                arrows[i].gameObject.SetActive(true);
                activeSlot = i;
                continue;
            }

            arrows[i].gameObject.SetActive(false);
        }

    }

    private void PlayerAbilities_OnSpiceChanged(object sender, PlayerAbilities.OnSpiceChangedArgs e)
    {
        spiceImages[activeSlot].sprite = e.spice.spiceIcon;
    }

    private void OnDisable()
    {
        spiceImages[0].sprite = blankSprite;
        spiceImages[1].sprite = blankSprite;
        spiceImages[2].sprite = blankSprite;
    }
}

