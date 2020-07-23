using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewSpice", menuName ="New Spice")]
public class Spice : ScriptableObject, ISellable
{
    public string spiceName;
    public Sprite spiceIcon;

    public void Sell(GameObject player)
    {
        player.GetComponent<PlayerAbilities>().AddNewSpice(this);
    }
}
