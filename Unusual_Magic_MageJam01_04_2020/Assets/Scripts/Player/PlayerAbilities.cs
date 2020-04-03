using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Cooldowns")]
    [SerializeField] float basicAttackCooldown = 0.4f;
    [SerializeField] float chickenCooldown = 6f;
    [SerializeField] float jellyCooldown = 3f;
    [Header("Cooldown Text")]
    [SerializeField] TextMeshProUGUI chickenCD;
    [SerializeField] TextMeshProUGUI jellyCD;
    [Header("Cast Place")]
    [SerializeField] Transform castPlace;
    [Header("Prefabs")]
    [SerializeField] GameObject jelly;
    [SerializeField] GameObject additionalAttackChicken;
    [SerializeField] GameObject basicAttackCookie;

    float currentTimeBasicAttack = 0f;
    float currentTimeChicken = 0f;
    float currentTimeJelly = 0f;

    void Update()
    {
        if(currentTimeChicken > 0)
        {
            chickenCD.text = ((int)currentTimeChicken).ToString();
        }
        else
        {
            chickenCD.gameObject.SetActive(false);
        }
        if (currentTimeJelly > 0)
        {
            jellyCD.text = ((int)currentTimeJelly).ToString();
        }
        else
        {
            jellyCD.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentTimeBasicAttack <=0)
        {
            GetComponent<Animator>().SetTrigger("shoot");
            currentTimeBasicAttack = basicAttackCooldown;
            Instantiate(basicAttackCookie, castPlace.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentTimeChicken <= 0)
        {
            GetComponent<Animator>().SetTrigger("shoot");
            chickenCD.gameObject.SetActive(true);
            currentTimeChicken = chickenCooldown;
            Instantiate(additionalAttackChicken, castPlace.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.E) && currentTimeJelly <= 0)
        {
            GetComponent<Animator>().SetTrigger("shoot");
            jellyCD.gameObject.SetActive(true);
            currentTimeJelly = jellyCooldown;
            Instantiate(jelly, castPlace.position, Quaternion.identity);
        }
        currentTimeBasicAttack -= Time.deltaTime;
        currentTimeChicken -= Time.deltaTime;
        currentTimeJelly -= Time.deltaTime;
    }
}
