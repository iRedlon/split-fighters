using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PlayerNum { Player1, Player2 }

public class UIManager : MonoBehaviour
{
    public Color player1Color = new Color(255f, 119f, 0f);
    public Color player2Color = new Color(244f, 255f, 0f);
    public Color player3Color = new Color(255f, 0f, 207f);
    public Color player4Color = new Color(0f, 241f, 255f);
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider player1Slider, player2Slider;
    
    [SerializeField] private Image team1LeftControllerIcon, team1RightControllerIcon;
    [SerializeField] private Image team2LeftControllerIcon, team2RightControllerIcon;

    private bool gameOver;

    void Awake()
    {
        team1LeftControllerIcon.color = player1Color;
        team1RightControllerIcon.color = player2Color;
        team2LeftControllerIcon.color = player3Color;
        team2RightControllerIcon.color = player4Color;
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

    public void EndGame()
    {
        timerText.text = "Game Over!";
        gameOver = true;
    }
}
