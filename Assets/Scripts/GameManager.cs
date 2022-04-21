using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    
    private float timeRemaining = 300f;

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

    public void EndGame()
    {
        uiManager.EndGame();
        Time.timeScale = 0;
    }
}
