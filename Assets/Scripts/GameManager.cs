using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    
    private float timeRemaining = 300f;

    public bool gameStarted = false;
    private bool gameOver = false;

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
                EndGame();
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

        foreach (FighterController fc in GetComponents<FighterController>())
        {
            fc.ResetGame();
        }
    }

    public void EndGame()
    {
        gameOver = true;
        uiManager.EndGame();
        Time.timeScale = 0;
    }
}
