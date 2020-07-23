using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] AudioSource hurtSource;
    [SerializeField] GameObject deathPanel;
    [SerializeField] float damageHighlightDuration = 0.3f;
    [SerializeField] float damageHighlightFallOff = 0.1f;
    public delegate void DamageTakenHandler(int currentHealth);
    public event DamageTakenHandler damageTakenEvent;

    Animator anim;
    Material material;
    int maxHealth = 4;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        material = GetComponent<SpriteRenderer>().material;
    }

    public void TakeDamage()
    {
        hurtSource?.Play();
        StartCoroutine(DamageHighlight());

        health--;
        if(tag == "Player")
        {
            CameraShaker.instance.Shake();
        }
        damageTakenEvent?.Invoke(health);

        if (health <= 0)
        {                     
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmgAmmount)
    {
        hurtSource.Play();
        health = Mathf.Max(0, health - dmgAmmount);
        StartCoroutine(DamageHighlight());
        if (tag == "Player")
        {
            CameraShaker.instance.Shake();
        }
        if (health <= 0)
        {  
            gameObject.SetActive(false);
        }
    }   

    public int GetHealthAmmount()
    {
        return health;
    }

    public void RestoreHealth()
    {
        health++;
        Mathf.Clamp(health, 0, 4);
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }
    void OnDisable()
    {
        if (tag == "Player")
        {
            if (deathPanel == null) return;
            deathPanel.SetActive(true);
        }
    }

    private void OnEnable()
    {
        material.SetFloat("_multiplier", 0);
    }

    IEnumerator DamageHighlight()
    {
        material.SetFloat("_multiplier", 1);
        yield return new WaitForSeconds(damageHighlightDuration);
        while (material.GetFloat("_multiplier") > 0)
        {
            material.SetFloat("_multiplier", material.GetFloat("_multiplier") - damageHighlightFallOff);
            yield return new WaitForSeconds(0.025f);
        }
        material.SetFloat("_multiplier", 0);

    }

    public void IncreaseMaxHP()
    {
        maxHealth++;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHP(int max)
    {
        maxHealth = max;
    }
}
