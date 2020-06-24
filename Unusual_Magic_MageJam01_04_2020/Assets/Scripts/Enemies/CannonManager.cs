﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] List<Transform> shootingStartPos = new List<Transform>();
    [SerializeField] float shootingInterval;
    [SerializeField] Vector2 shootingDirection;
    [SerializeField] float ballXSpeed;
    [SerializeField] float ballYSpeed;
    [SerializeField] float lifeTime;
    bool canShoot = true;
    float currentShootCD = 0;
    int cannonIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if (!canShoot) return;

        if(currentShootCD <= 0)
        {
            
            GameObject go = Instantiate(projectile, shootingStartPos[cannonIndex].position, Quaternion.identity);
            go.GetComponent<CannonBall>().SetUpCannon(shootingDirection, ballXSpeed, ballYSpeed, lifeTime);
            currentShootCD = shootingInterval;
            cannonIndex++;
            if(cannonIndex > shootingStartPos.Count-1)
            {
                cannonIndex = 0;
            }
        }
        else
        {
            currentShootCD -= Time.deltaTime;
        }        
    }   

    public void StopShooting()
    {
        canShoot = false;
    }

}
