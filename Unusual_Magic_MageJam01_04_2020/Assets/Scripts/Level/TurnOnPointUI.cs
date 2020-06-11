﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnPointUI : MonoBehaviour
{
    [SerializeField] GameObject pointUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pointUI.SetActive(true);
    }
}
