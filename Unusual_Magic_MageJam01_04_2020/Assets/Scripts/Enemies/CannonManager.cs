using System.Collections;
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
    [SerializeField] bool randomShooting = false;
    bool canShoot = true;
    float currentShootCD = 0;
    int cannonIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (!canShoot) return;
        if (randomShooting)
        {
            if (currentShootCD <= 0)
            {

                GameObject go = Instantiate(projectile, shootingStartPos[Random.Range(0, shootingStartPos.Count)].position, Quaternion.identity);
                go.GetComponent<CannonBall>().SetUpCannon(shootingDirection, ballXSpeed, ballYSpeed, lifeTime);
                currentShootCD = shootingInterval;

            }
            else
            {
                currentShootCD -= Time.deltaTime;
            }
        }
        else
        {
            if (currentShootCD <= 0)
            {

                GameObject go = Instantiate(projectile, shootingStartPos[cannonIndex].position, Quaternion.identity);
                go.GetComponent<CannonBall>().SetUpCannon(shootingDirection, ballXSpeed, ballYSpeed, lifeTime);
                currentShootCD = shootingInterval;
                cannonIndex++;
            }
            else
            {
                currentShootCD -= Time.deltaTime;
            }

            if (cannonIndex > shootingStartPos.Count - 1)
            {
                cannonIndex = 0;
            }
        }
    }   

    public void StopShooting()
    {
        canShoot = false;
    }

}
