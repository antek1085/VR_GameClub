using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetsController : MonoBehaviour
{
    [SerializeField] float allPoints;
    ShootTarget _shootTarget;

    bool countTime;
    [SerializeField] float playTime;
    public float passedTime;
    int intPassedTime;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI scoresText;
    List<ShootTarget> shootTargetsList = new List<ShootTarget>();
    AudioSource audioSource;
    [SerializeField] AudioClip endingMusic;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        countTime = false;
        timeText.text = playTime.ToString();
        pointsText.text = allPoints.ToString();
        for (int i = 0; i < transform.childCount; i++)
        {
            shootTargetsList.Add(transform.GetChild(i).GetComponentInChildren<ShootTarget>()); 
        }
    }

    void Update()
    {
        if (countTime)
        {
            passedTime -= Time.deltaTime;
            intPassedTime = (int)passedTime;
            timeText.text = intPassedTime.ToString();
            scoresText.text = allPoints.ToString();
            if (passedTime < 0)
            {
                EndGame();
            }
            pointsText.text = allPoints.ToString();
        }
    }

    void EndGame()
    {
        audioSource.PlayOneShot(endingMusic);
        StopAllCoroutines();
        countTime = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            _shootTarget = transform.GetChild(i).GetComponentInChildren<ShootTarget>();
            if (_shootTarget.isActive == true)
            {
                _shootTarget.DisableTarget();
            }
        }
    }


    public void StartGame()
    {
        StopAllCoroutines();
        countTime = true;
        passedTime = playTime;
        for (int i = 0; i < transform.childCount; i++)
        {
            _shootTarget = transform.GetChild(i).GetComponentInChildren<ShootTarget>();
                _shootTarget.DisableTarget();
        }
        StartCoroutine(ActiveObjects());
    }

    IEnumerator ActiveObjects()
    {
        yield return new WaitForSeconds(4f);

        int count = 0; 
        
        for (int i = 0; i < shootTargetsList.Count; i++)
        {
            if (shootTargetsList[i].isActive == false)
            { 
                count++;
            }
        }
        if (count == 0)
        {
            StartCoroutine(ActiveObjects());
        }
        else
        {
            ChooseTarget();
        }
    }

    void ChooseTarget()
    {
        
            int i = Random.Range(0, shootTargetsList.Count);
            _shootTarget = shootTargetsList[i];
            if (_shootTarget.isActive == false)
            {
                _shootTarget.EnableTarget();
                StartCoroutine(ActiveObjects());
            }
            else
            {
                    ChooseTarget(); 
            }
    }
    
    public void ChangePoints(float points)
    {
        allPoints += points;
    }
}
