using System;
using UnityEngine;

public class ShootTarget : MonoBehaviour,IDamageable
{
    public bool isActive;
    [SerializeField] Transform target;
    private TargetsController controller;
    [SerializeField] float points;
    float time;
    
    [Header("Sounds")]
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;
    
    
    public enum TargetType
    {
        Friendly,
        Enemy
    }

    public TargetType targetType;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponentInParent<TargetsController>();
        
    }
    void Update()
    {
        if (isActive && TargetType.Friendly == targetType)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                DisableTarget();
            }
        }
    }
    public void Damageable()
    {
        if (isActive)
        {
            audioSource.PlayOneShot(hitSound);
            switch (targetType)
            {

                case TargetType.Friendly:
                    controller.ChangePoints(-points);
                    break;
                case TargetType.Enemy:
                    controller.ChangePoints(points);
                    break;
                default:
                    break;
            }  
        }
        DisableTarget();
    }


    public void EnableTarget()
    {
        isActive = true;
        var rotation = target.rotation.eulerAngles;
        rotation.x = 0;
        target.localRotation = Quaternion.Euler(rotation);
        time = 10f;
    }

    public void DisableTarget()
    {
        isActive = false;
        var rotation = target.rotation.eulerAngles;
        rotation.x = -90;
        target.localRotation = Quaternion.Euler(rotation);
    }
}
