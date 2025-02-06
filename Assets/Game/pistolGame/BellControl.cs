using System;
using UnityEngine;

public class BellControl : MonoBehaviour
{
    [SerializeField] TargetsController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            controller.StartGame();
        }
    }
}
