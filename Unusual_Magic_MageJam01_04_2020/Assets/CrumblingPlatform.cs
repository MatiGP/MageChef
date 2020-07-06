using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] ParticleSystem crumblingEffect;
    [SerializeField] Sprite[] crumbleSprites;
    [SerializeField] float crumbleDelay = 0.5f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SpriteRenderer spriteRenderer;

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
        for(int i = 0; i < crumbleSprites.Length; i++)
        {
            spriteRenderer.sprite = crumbleSprites[i];
            audioSource.Play();
            crumblingEffect.Play();
            yield return new WaitForSeconds(crumbleDelay);
        }       
        gameObject.SetActive(false);
    }
}
