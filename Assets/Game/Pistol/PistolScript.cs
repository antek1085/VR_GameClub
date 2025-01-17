using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class PistolScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    
    [SerializeField] int ammunition;
    [SerializeField] float timeBetweenShoots;
    private HapticImpulsePlayer xrHapticImpulsePlayer;

    private bool canHeShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        canHeShoot = true;
    }
    public void TryToShoot()
    {
        if (canHeShoot == true && ammunition != 0)
        {
            //get xrHapticImpulsePlayer (which hand)
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        GameObject shootedBullet;
        canHeShoot = false;
        ammunition -= 1;
        shootedBullet = Instantiate(bullet, shootPoint.position, Quaternion.Euler(0,0,90));
        shootedBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 2000f);
        xrHapticImpulsePlayer.SendHapticImpulse(1, 0.2f);
        yield return new WaitForSeconds(timeBetweenShoots);
        canHeShoot = true;
        StopCoroutine(Shoot());
    }


    public void RaycastHand()
    {
        
    }
}
