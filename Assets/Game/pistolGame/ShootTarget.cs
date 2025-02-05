using System;
using UnityEngine;

public class ShootTarget : MonoBehaviour,IDamageable
{
    [SerializeField] Material friendlyMaterial, enemyMaterial;
    public bool isActive;
    private Transform target;
    private TargetsController controller;
    [SerializeField] float points;
    
    
    public enum TargetType
    {
        Friendly,
        Enemy
    }

    public TargetType targetType;


    void Awake()
    {
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
    public void Damageable()
    {
        if (isActive)
        {
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
        target.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0),1f);
        
    }

    public void DisableTarget()
    {
        isActive = false;
        target.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), 1f);
    }
}
