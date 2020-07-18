using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float speed;
    Vector2 startPos;
    Vector2 endPos;

    bool goBack;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position;
        endPos.y += height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, endPos) < 0.1f)
        {
            goBack = true;
        }
        else if (Vector2.Distance(transform.position, startPos) < 0.1f)
        {
            goBack = false;
        }

        if (!goBack)
        {
            transform.position = Vector2.Lerp(transform.position, endPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, startPos, speed * Time.deltaTime);
        }
    }
}
