using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierRotator : MonoBehaviour
{
    [SerializeField] float barrierRotateSpeed;
    [SerializeField] GameObject barrier;

    // Update is called once per frame
    void Update()
    {
        barrier.transform.Rotate(0, 0, barrierRotateSpeed * Time.deltaTime);
    }
}
