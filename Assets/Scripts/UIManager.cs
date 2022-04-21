using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PlayerNum { Player1, Player2 }

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider player1Slider, player2Slider;
    
    public void UpdateTimerText(float timeRemaining)
    {
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

        if (playerNum == PlayerNum.Player1)
        {
            player1Slider.value = (float) health / (float) maxHealth;
        }
        
        if (playerNum == PlayerNum.Player2)
        {
            player2Slider.value = (float) health / (float) maxHealth;
        }
    }

    public void EndGame()
    {
        timerText.text = "Game Over!";
    }
}
