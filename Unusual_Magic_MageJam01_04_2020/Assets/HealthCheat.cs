using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCheat : MonoBehaviour
{
    [SerializeField] GameObject health;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Instantiate(health, transform.position, Quaternion.identity);
        }
    }
}
