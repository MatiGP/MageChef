using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = transform.parent;

        Renderer parentRenderer = parent.GetComponent<Renderer>();

        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerID = parentRenderer.sortingLayerID;
        renderer.sortingOrder = parentRenderer.sortingOrder + 1;

    }

    
}
