using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PlayerNum { Player1, Player2 }

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider player1Slider, player2Slider;
    
    [SerializeField] private Image team1LeftControllerIcon, team1RightControllerIcon;
    [SerializeField] private Image team2LeftControllerIcon, team2RightControllerIcon;

    private bool gameOver;
    public bool ready;
    
    private float readyTimer = 3f;
    
    public GameObject mainMenuCanvas, creditsCanvas, pregameCanvas, readyCanvas, gameCanvas;

    // TODO: Trigger on 'Start' button on main menu
    public void MainMenuStart()
    {
        pregameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    // TODO: Trigger on 'Credits' button on main menu
    public void Credits()
    {
        creditsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    // TODO: Trigger on 'Quit' button on main menu
    public void QuitGame()
    {
        Application.Quit();
    }

    // TODO: Trigger on 'Back' button on credits menu
    public void BackToMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    // TODO: Trigger on P2 controller connection
    public void Ready()
    {
        readyCanvas.SetActive(true);
        pregameCanvas.SetActive(false);

        ready = true;
    }

    public void StartGame()
    {
        gameCanvas.SetActive(true);
        readyCanvas.SetActive(false);
        pregameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }

    public void SetPlayerColors(Color p1Color, Color p2Color, Color p3Color, Color p4Color)
    {
        team1LeftControllerIcon.color = p1Color;
        team1RightControllerIcon.color = p2Color;
        team2LeftControllerIcon.color = p3Color;
        team2RightControllerIcon.color = p4Color;
    }
    
    public void UpdateTimerText(float timeRemaining)
    {
        if (gameOver)
        {
            return;
        }
        
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        
        if (seconds < 10) {
            timerText.text = $"{minutes}:0{seconds}";
        } else {
            timerText.text = $"{minutes}:{seconds}";
        }
    }

    public void UpdateHealthSlider(GameObject fighter, float health, float maxHealth)
    {
        PlayerNum playerNum = fighter.name == "Character" ? PlayerNum.Player1 : PlayerNum.Player2;
        float ratio = (float) health / (float) maxHealth;

        if (playerNum == PlayerNum.Player1)
        {
            player1Slider.value = ratio;
        }
        
        if (playerNum == PlayerNum.Player2)
        {
            player2Slider.value = ratio;
        }
    }

    public void ResetGame()
    {
        gameOver = false;
    }

    public void EndGame(FighterName winner)
    {
        if (winner == FighterName.RedFighter)
        {
            timerText.text = "Red wins!";
        }
        else if (winner == FighterName.BlueFighter)
        {
            timerText.text = "Blue wins!";
        } 
        else if (winner == FighterName.Neither)
        {
            timerText.text = "Draw!";
        }
        
        gameOver = true;
    }

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        if (ready)
        {
            if (readyTimer > 0)
            {
                readyTimer -= Time.deltaTime;
            }
            else
            {
                gameManager.StartGame();
                StartGame();

                ready = false;
            }
        }
    }
}
