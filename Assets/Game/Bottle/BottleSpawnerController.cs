using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class BottleSpawnerController : MonoBehaviour
{
    List<Transform> bottleSpawner = new List<Transform>();
    [SerializeField] float spawnTime;
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bottleSpawner.Add(transform.GetChild(i));
        }
      
    }
    void Start()
    {
        StartCoroutine(ChooseSpawnPoint());
    }

    // Update is called once per frame
    IEnumerator ChooseSpawnPoint()
    {
        int spawnPointNumber;
        spawnPointNumber = Random.Range(0, bottleSpawner.Count - 1);
        Debug.Log(spawnPointNumber);
        bottleSpawner[spawnPointNumber].GetComponent<BottleSpawner>().SpawnBottle();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(ChooseSpawnPoint());
    }
}
