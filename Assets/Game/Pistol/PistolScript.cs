using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PistolScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    
    [SerializeField] int ammunition;
    Ammo _ammo;
    [SerializeField] float timeBetweenShoots;

    [SerializeField] bool canHeShoot;
    XRBaseInteractor socketInteractor;
    
    string controllerName;
    

    void Awake()
    {
        /*socketActionLeft.action.Enable();
        socketActionLeft.action.performed += RemoveMagazine;
        socketActionRight.action.Enable();
        socketActionRight.action.performed += RemoveMagazine;*/
        
        canHeShoot = true;
        
        XRBaseInteractable xrBaseInteractable = GetComponent<XRBaseInteractable>();
        xrBaseInteractable.activated.AddListener(TriggerHapticFeedback);
        
        /*socketInteractor.selectEntered.AddListener(AddAmmunition);
        socketInteractor.selectExited.AddListener(RemoveAmmunition);*/
    }

    /*public void TryToShoot()
    {
       
    }*/

    IEnumerator Shoot()
    {
        GameObject shootedBullet;
        canHeShoot = false;
        ammunition -= 1;
        shootedBullet = Instantiate(bullet, shootPoint.position, Quaternion.Euler(0,0,90));
        shootedBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 2000f);
        
        yield return new WaitForSeconds(timeBetweenShoots);
        canHeShoot = true;
        StopCoroutine(Shoot());
        
    }

    public void TriggerHapticFeedback(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor);
        }
    }

    public void TriggerHaptic(XRBaseInteractor interactor)
    {
        controllerName = interactor.transform.parent.name;
        if (canHeShoot == true && ammunition > 0)
        {
             var test =interactor.GetComponentInParent<HapticImpulsePlayer>().SendHapticImpulse(0.2f, 0.2f);
             Debug.Log(test);
            StartCoroutine(Shoot());
        }
    }

    
    public void AddAmmunition(SelectEnterEventArgs arg0)
    {
        _ammo = arg0.interactableObject.transform.GetComponent<Ammo>();
    }
    public void RemoveAmmunition(SelectExitEventArgs arg0)
    {
        _ammo = null;
    }

    void RemoveMagazine(InputAction.CallbackContext context)
    {
        switch (controllerName)
        {
            case "Left Controller":
                break;
            case "Right Controller":
                
                break;
            
            default:
                break;
        }
    }
   
}
