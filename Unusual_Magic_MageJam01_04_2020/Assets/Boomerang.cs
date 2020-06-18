using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] float boomerangSpeed;
    [SerializeField] float maxDistanceFromPlayer;

    Transform playerPosition;
    BoxCollider2D cc2d;

    bool returnBoomerang;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        playerPosition = PlayerPoints.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(startPos, transform.position) < maxDistanceFromPlayer && !returnBoomerang)
        {
            transform.Translate(boomerangSpeed * Time.deltaTime * transform.localScale.x, 0, 0);
        }
        else
        {
            returnBoomerang = true;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, boomerangSpeed * Time.deltaTime);

        }

        if(returnBoomerang)
        {
            cc2d.enabled = false;

            if (Vector2.Distance(transform.position, playerPosition.position) < 0.4f)
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void ReturnBoomerang()
    {
        returnBoomerang = true;
    }
}
