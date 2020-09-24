using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text PointsText;
    [SerializeField] PlayerMover playerMover;

    private IEnumerator coroutine;
    private int TotalPoints;
    public void StartLevel() 
    {
        GameOverPanel.SetActive(false);
    }
    public void RestartLevel() 
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        GameOverPanel.SetActive(false);
        playerMover.Activate();
    }

    public void GameOver()
    {
        TotalPoints = LevelManager.points;
        GameOverPanel.SetActive(true);
        coroutine = AddPoints();
        StartCoroutine(coroutine);
    }

    IEnumerator AddPoints() 
    {
        PointsText.text = "0";
        int currentPoints = 0;
        while (currentPoints < TotalPoints) 
        {
            int add = (TotalPoints - currentPoints) / 10;
            if (add < 1)
                add = 1;
            currentPoints += add;
            PointsText.text = currentPoints.ToString();
            yield return null;
            if (currentPoints >= TotalPoints) 
            {
                StopCoroutine(coroutine);
            }
        }
    }
}
