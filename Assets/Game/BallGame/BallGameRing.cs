using System;
using UnityEngine;

public class BallGameRing : MonoBehaviour
{
    int points;
    bool isGameStarted = false;
    [SerializeField] float playTime;
    float timeLeft;

    void Update()
    {
        if (isGameStarted)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                isGameStarted = false;
            }
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && isGameStarted)
        {
            points++;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        timeLeft = playTime;
    }
}
