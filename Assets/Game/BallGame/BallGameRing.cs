using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallGameRing : MonoBehaviour
{
    int points;
    bool isGameStarted = false;
    [SerializeField] float playTime;
    int IntPlayTime;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI timeText;
    float timeLeft;
    int timePlayed;
    
    [SerializeField] GameObject highScoreList;

    void Update()
    {
        if (isGameStarted)
        {
            timeLeft -= Time.deltaTime;
            IntPlayTime = (int)timeLeft;
            timeText.text = IntPlayTime.ToString();
            pointsText.text = points.ToString();
            if (timeLeft < 0)
            {
                timeText.text = "60";
                points = 0;
                pointsText.text = points.ToString();
                isGameStarted = false;
                
                if(timePlayed > highScoreList.transform.childCount-1) timePlayed = 0;
                highScoreList.transform.GetChild(timePlayed).GetComponentInChildren<TextMeshProUGUI>().text = points.ToString();
                timePlayed += 1;
            }
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            switch (isGameStarted)
            {
                case true:
                    points++;
                    break;
                case false:
                    StartGame();
                    points++;
                    isGameStarted = true;
                    break;
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        timeLeft = playTime;
    }
}
