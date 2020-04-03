using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] GameObject bars;

    private void OnDestroy()
    {
        Destroy(bars);
    }
}
