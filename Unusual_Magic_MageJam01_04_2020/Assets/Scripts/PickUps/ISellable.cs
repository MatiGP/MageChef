using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISellable
{
    void Sell(GameObject player);
    Sprite GetIcon();
}
