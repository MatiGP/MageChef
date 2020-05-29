using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    bool isFrozen;
    [SerializeField] float freezeDuration;
    [SerializeField] Sprite frozenPipe;
    [SerializeField] Sprite unfrozenPipe;
    [SerializeField] ParticleSystem psys;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FrozenCookie")
        {
            print("Dostałem Ciastkiem!");
            Freeze();
        }
    }

    void Freeze()
    {
        isFrozen = true;
        StartCoroutine(FreezeForDuration(freezeDuration));
    }

    void SwapSprites()
    {
        sr.sprite = isFrozen ? sr.sprite = frozenPipe : sr.sprite = unfrozenPipe;
    }

    IEnumerator FreezeForDuration(float duration)
    {
        SwapSprites();
        psys.gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(duration);
        isFrozen = false;
        GetComponent<BoxCollider2D>().enabled = true;
        psys.gameObject.SetActive(true);
        SwapSprites();
        
    }
}
