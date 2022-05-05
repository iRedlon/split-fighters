using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FighterName { RedFighter, BlueFighter, Neither };

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    
    private float timeRemaining = 300f;

    public bool gameStarted = false;
    public bool gameOver = false;

    public int blueRounds, redRounds;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (gameStarted)
        {
            uiManager.UpdateTimerText(timeRemaining);
        
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                EndGame(FighterName.Neither);
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    public void ResetGame()
    {
        gameOver = false;
        timeRemaining = 300f;
        uiManager.ResetGame();
        Time.timeScale = 1f;

        foreach (FighterController fc in FindObjectsOfType<FighterController>())
        {
            fc.ResetGame();
        }
    }

    public void EndRound(FighterName winner)
    {
        if (winner == FighterName.RedFighter)
        {
            redRounds++;

            if (redRounds == 2)
            {
                EndGame(FighterName.RedFighter);
            }
            else
            {
                ResetGame();
            }
        }

        if (winner == FighterName.BlueFighter)
        {
            blueRounds++;

            if (blueRounds == 2)
            {
                EndGame(FighterName.BlueFighter);
            }
            else
            {
                ResetGame();
            }
        }
    }

    public void EndGame(FighterName winner)
    {
        foreach (FighterController fc in FindObjectsOfType<FighterController>())
        {
            fc.ResetPosition();
        }
        
        uiManager.EndGame(winner);
        gameOver = true;
        gameStarted = false;
        // Time.timeScale = 0;
    }

    public void RestartGame()
    {
        redRounds = 0;
        blueRounds = 0;
        gameStarted = true;
        ResetGame();
    }
}
