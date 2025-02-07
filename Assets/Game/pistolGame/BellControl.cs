using System;
using UnityEngine;

public class BellControl : MonoBehaviour
{
    [SerializeField] TargetsController controller;
    [SerializeField] AudioClip  audioClip;
    AudioSource audioSource;


    void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            controller.StartGame();
            if (audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}
