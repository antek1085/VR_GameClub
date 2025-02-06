using System;
using UnityEngine;

public class AmmoBin : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] Transform ammmoSpawn;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            Destroy(other.gameObject);
            Instantiate(ammo, ammmoSpawn.transform.position, ammmoSpawn.transform.rotation);
        }   
    }
}
