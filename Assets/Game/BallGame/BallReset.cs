using System;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            other.GetComponent<BallScript>().isItVaiable = true;
        }
    }
}
