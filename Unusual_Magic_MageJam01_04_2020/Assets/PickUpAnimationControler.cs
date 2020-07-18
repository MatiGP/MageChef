using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpAnimationControler : MonoBehaviour
{
    [SerializeField] PlayerAbilities playerAbilities;
    [SerializeField] Animator animator;
    [SerializeField] Image pickUpImage;
    // Start is called before the first frame update
    void Start()
    {
        playerAbilities.OnRecipeCollected += PlayerAbilities_OnRecipeCollected;
        playerAbilities.OnSpicePicked += PlayerAbilities_OnSpicePicked;
    }

    private void PlayerAbilities_OnSpicePicked(object sender, PlayerAbilities.OnSpicePickedUp e)
    {
        pickUpImage.sprite = e.spiceIcon;
        animator.SetTrigger("pickUp");
    }

    private void PlayerAbilities_OnRecipeCollected(object sender, PlayerAbilities.OnRecipeCollectedArgs e)
    {
        pickUpImage.sprite = e.recipe.recipeIcon;
        animator.SetTrigger("pickUp");
    }   
}
