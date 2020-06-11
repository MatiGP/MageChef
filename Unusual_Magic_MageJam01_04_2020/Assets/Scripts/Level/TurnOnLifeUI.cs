using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLifeUI : MonoBehaviour
{
    [SerializeField] GameObject healthUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        healthUI.SetActive(true);
    }
}
