using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlingCookiePillar : MonoBehaviour
{
    [SerializeField] float pillarRiseHeight;
    [SerializeField] float speed;

    Vector2 startPos;
    Vector2 endPos;

    bool rise = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector2(startPos.x, startPos.y);
        endPos.y += pillarRiseHeight;
    }

    private void Update()
    {
        if (rise && transform.position != (Vector3)endPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        }else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }
    }

    public void Rise()
    {
        rise = true;
    }

    public void Fall()
    {
        rise = false;
    }
}
