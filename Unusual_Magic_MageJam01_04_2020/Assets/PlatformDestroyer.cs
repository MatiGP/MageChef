using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyPlatform());
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
