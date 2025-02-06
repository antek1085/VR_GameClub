using System;
using UnityEngine;

public class ShootTarget : MonoBehaviour,IDamageable
{
    [SerializeField] Material friendlyMaterial, enemyMaterial;
    public bool isActive;
    private Transform target;
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
        target = GetComponentInParent<Transform>();
        controller = GetComponentInParent<TargetsController>();
        
        switch (targetType)
        {
            case TargetType.Friendly:
                GetComponent<MeshRenderer>().material = friendlyMaterial;
                break;
            case TargetType.Enemy:
                GetComponent<MeshRenderer>().material = enemyMaterial;
                break;
            default:
               break;
        }
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
        rotation.z = 0;
        target.rotation = Quaternion.Euler(rotation);
        time = 10f;
    }

    public void DisableTarget()
    {
        isActive = false;
        var rotation = target.rotation.eulerAngles;
        rotation.z = 90;
        target.rotation = Quaternion.Euler(rotation);
    }
}
