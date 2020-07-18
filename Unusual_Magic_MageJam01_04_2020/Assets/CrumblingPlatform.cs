using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] ParticleSystem crumblingEffect;
    [SerializeField] Sprite[] crumbleSprites;
    [SerializeField] float crumbleDelay = 0.5f;
    [SerializeField] float timeToRebuild = 2f;
    [SerializeField] float platformRebuildDelay = 2f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D platformCollider;


    bool isCrumbling;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !isCrumbling)
        {
            isCrumbling = true;
            StartCoroutine(Crumble());
        }
    }

    IEnumerator Crumble()
    {
        for(int i = 1; i < crumbleSprites.Length; i++)
        {
            spriteRenderer.sprite = crumbleSprites[i];
            audioSource.Play();
            crumblingEffect.Play();
             
            yield return new WaitForSeconds(crumbleDelay);
        }

        platformCollider.enabled = false;
        spriteRenderer.sprite = null;

        yield return new WaitForSeconds(timeToRebuild);

        for (int i = crumbleSprites.Length - 1; i >= 0; i--)
        {
            yield return new WaitForSeconds(platformRebuildDelay);

            spriteRenderer.sprite = crumbleSprites[i];

            if (i == 0)
            {
                platformCollider.enabled = true;
                isCrumbling = false;
            }
            
        }       
        
    }

    
}
