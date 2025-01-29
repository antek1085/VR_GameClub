using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Feedback;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PistolScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    
    [SerializeField] int ammunition;
    [SerializeField] float timeBetweenShoots;

    [SerializeField] bool canHeShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        canHeShoot = true;
        XRBaseInteractable xrBaseInteractable = GetComponent<XRBaseInteractable>();
        xrBaseInteractable.activated.AddListener(TriggerHapticFeedback);
    }
    public void TryToShoot()
    {
        /*if (canHeShoot == true && ammunition != 0)
        {
            StartCoroutine(Shoot());
        }*/
    }

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
        Debug.Log(eventArgs);
        if (eventArgs.interactorObject is XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor);
        }
    }

    public void TriggerHaptic(XRBaseInteractor interactor)
    {
        if (canHeShoot == true && ammunition != 0)
        {
            interactor.GetComponentInParent<HapticImpulsePlayer>().SendHapticImpulse(0.2f, 0.2f, 10);
            StartCoroutine(Shoot());
        }
    }
   
}
