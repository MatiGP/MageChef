using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPickUp : MonoBehaviour, ISellable
{
    public Sprite icon;

    public void Sell(GameObject player)
    {
        player.GetComponent<HealthController>().IncreaseHP();
    }
}
