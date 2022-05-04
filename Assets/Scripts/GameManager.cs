using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    
    private float timeRemaining = 300f;
    private bool gameOver = false;

    public AudioSource audioSource;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
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

    public void ResetGame()
    {
        gameOver = false;
        timeRemaining = 300f;
        uiManager.ResetGame();
        Time.timeScale = 1f;
        audioSource.Play();


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
