using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPoints : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("score", 0);
    }
}
