using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPickUp : MonoBehaviour, ISellable
{
    public Sprite icon;

    public Sprite GetIcon()
    {
        return icon;
    }

    public void Sell(GameObject player)
    {
        player.GetComponent<HealthController>().IncreaseHP();
        player.GetComponent<Health>().SetHealth(player.GetComponent<Health>().GetHealthAmmount() + 1);
    }

    
}
