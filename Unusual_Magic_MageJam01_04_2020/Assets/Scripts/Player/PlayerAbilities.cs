using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;

public class PlayerAbilities : MonoBehaviour
{
    public event EventHandler<OnSpiceChangedArgs> OnSpiceChanged;
    public event EventHandler<OnSpiceSlotChangedArgs> OnSpiceSlotChanged;
    public event EventHandler<OnSpellCraftedArgs> OnSpellCrafted;
    public event EventHandler<OnRecipeCollectedArgs> OnRecipeCollected;
    public event EventHandler<OnSpicePickedUp> OnSpicePicked;

    public class OnSpiceChangedArgs : EventArgs
    {
        public Spice spice;
    }
    public class OnSpiceSlotChangedArgs : EventArgs
    {
        public int spiceSlot;
    }
    public class OnSpellCraftedArgs : EventArgs
    {
        public Recipe recipe;
    }
    public class OnRecipeCollectedArgs : EventArgs
    {
        public Recipe recipe;
    }
    public class OnSpicePickedUp : EventArgs
    {
        public Sprite spiceIcon;
        public string spiceName;
    }
    
    int numOfRecipe = 0;

    public Recipe[] unlockedRecipes = new Recipe[8];
    [SerializeField] GameObject spellCraftingUI;

    Dictionary<Spice, int> ownedSpices = new Dictionary<Spice, int>();
    List<Spice> ownedSpiceList = new List<Spice>();
    Spice[] spellCraftingSpices;

    int spiceSlot = 0;
    int spiceIndex = 0;

    bool isSpellCrafting = false;
    [SerializeField] CinemachineVirtualCamera spellCraftingCam;
    [SerializeField] GameObject cookingPot;
    [SerializeField] AudioSource spellCompletedAudioSource;
    [SerializeField] AudioSource spiceChangedAudioSource;
    [SerializeField] AudioSource spellFailedAudioSource;

    PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSpellCrafting && pc.canJump)
        {
            spellCraftingSpices = new Spice[3];
            isSpellCrafting = true;
            pc.DisableMovement();
            pc.SpellCraft(true);
            spellCraftingCam.gameObject.SetActive(true);
            cookingPot.gameObject.SetActive(true);
            GetSpices();
            spellCraftingUI.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.E) && isSpellCrafting && pc.canJump)
        {
            isSpellCrafting = false;
            pc.EnableMovement();
            cookingPot.gameObject.SetActive(false);
            pc.SpellCraft(false);
            spellCraftingCam.gameObject.SetActive(false);
            spellCraftingUI.SetActive(false);
        }

        if (isSpellCrafting)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (spiceSlot - 1 < 0)
                {
                    spiceSlot = 2;
                }
                else
                {
                    spiceSlot -= 1;
                }

                OnSpiceSlotChanged?.Invoke(this, new OnSpiceSlotChangedArgs() { spiceSlot = spiceSlot });
                spiceChangedAudioSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (spiceSlot + 1 > 2)
                {
                    spiceSlot = 0;
                }
                else
                {
                    spiceSlot += 1;
                }

                OnSpiceSlotChanged?.Invoke(this, new OnSpiceSlotChangedArgs() { spiceSlot = spiceSlot });
                spiceChangedAudioSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (ownedSpiceList.Count != 0)
                {
                    if (spiceIndex + 1 > ownedSpiceList.Count - 1)
                    {
                        spiceIndex = 0;
                    }
                    else
                    {
                        spiceIndex += 1;
                    }
                    spellCraftingSpices[spiceSlot] = ownedSpiceList[spiceIndex];
                }

                OnSpiceChanged?.Invoke(this, new OnSpiceChangedArgs() { spice = ownedSpiceList[spiceIndex] });
                spiceChangedAudioSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (ownedSpiceList.Count != 0)
                {
                    if (spiceIndex - 1 < 0)
                    {
                        spiceIndex = ownedSpiceList.Count - 1;
                    }
                    else
                    {
                        spiceIndex -= 1;
                    }
                    spellCraftingSpices[spiceSlot] = ownedSpiceList[spiceIndex];
                }

                OnSpiceChanged?.Invoke(this, new OnSpiceChangedArgs() { spice = ownedSpiceList[spiceIndex] });
                spiceChangedAudioSource.Play();
            }

        }

        if (Input.GetKeyDown(KeyCode.R) && isSpellCrafting)
        {
            for (int i = 0; i < 3; i++)
            {
                if (spellCraftingSpices[i] == null)
                {
                    return;
                }
            }

            foreach (Recipe recipe in unlockedRecipes)
            {
                if (CheckForCorrectRecipe(recipe))
                {

                    if (CheckForCorrectAmountOfSpices(recipe))
                    {
                        OnSpellCrafted?.Invoke(this, new OnSpellCraftedArgs() { recipe = recipe });
                        foreach(Ingredient ingredient in recipe.ingredients)
                        {
                            ownedSpices[ingredient.requiredSpice] -= ingredient.requiredAmount;
                        }
                        foreach(Spice s in ownedSpices.Keys)
                        {
                            OnSpicePicked?.Invoke(this, new OnSpicePickedUp { spiceIcon = s.spiceIcon, spiceName = s.spiceName });
                        }
                        spellCompletedAudioSource.Play();
                        break;
                    }
                    else
                    {
                        spellFailedAudioSource.Play();
                        break;
                    }
                    
                }
            }


        }
    }

    private bool CheckForCorrectRecipe(Recipe recipe)
    {
        for (int i = 0; i < 3; i++)
        {
            if (recipe.ingredients[i].requiredSpice != spellCraftingSpices[i])
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckForCorrectAmountOfSpices(Recipe recipe)
    {
        foreach(Ingredient ingredient in recipe.ingredients)
        {
            if(ownedSpices[ingredient.requiredSpice] < ingredient.requiredAmount)
            {
                return false;
            }
        }

        return true;
    }

    public bool HasSpice(Spice spice)
    {
        return ownedSpices.ContainsKey(spice);
    }

    public void AddNewSpice(Spice spice)
    {
        ownedSpices.Add(spice, 1);
        OnSpicePicked?.Invoke(this, new OnSpicePickedUp { spiceIcon = spice.spiceIcon, spiceName = spice.spiceName });
    }

    public void AddSpice(Spice spice)
    {
        ownedSpices[spice]++;
        OnSpicePicked?.Invoke(this, new OnSpicePickedUp { spiceIcon = spice.spiceIcon, spiceName = spice.spiceName });

    }

    void GetSpices()
    {
        ownedSpiceList.Clear();

        foreach (Spice spice in ownedSpices.Keys)
        {
            ownedSpiceList.Add(spice);
        }
    }

    public void AddNewRecipe(Recipe recipe)
    {
        unlockedRecipes[numOfRecipe] = recipe;
        numOfRecipe++;
        OnRecipeCollected?.Invoke(this, new OnRecipeCollectedArgs() { recipe = recipe });
    }

    public Dictionary<Spice, int> GetOwnedSpices()
    {
        return ownedSpices;
    }

    public void SetDict(Dictionary<Spice, int> d)
    {
        ownedSpices = d;
    }

    public void ResetRecipeCounter()
    {
        numOfRecipe = 0;
    }
}

