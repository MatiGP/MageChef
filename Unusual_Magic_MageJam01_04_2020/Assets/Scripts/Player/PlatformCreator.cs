using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] Vector3 offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colliders")
        {
            GetComponent<Boomerang>().ReturnBoomerang();
            GameObject go = Instantiate(platform, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(transform.localScale.x * go.transform.localScale.x, go.transform.localScale.y, 1);
            go.transform.position = transform.position + (offset * go.transform.localScale.x);
        }
    }
}
