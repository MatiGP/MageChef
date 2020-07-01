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

    Animator anim;
    Material material;

    private void Start()
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


        if(health <= 0)
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
}
