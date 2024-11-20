using System;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField] GameObject bottle;
    public float throwPower;


    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SpawnBottle();
        }
    }

    public void SpawnBottle()
    {
        GameObject spawnedBottle;
        spawnedBottle = Instantiate(bottle, transform.position, this.transform.rotation);
        spawnedBottle.GetComponent<Rigidbody>().AddForce(this.transform.up * throwPower);
    }
}
