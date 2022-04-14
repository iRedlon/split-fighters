using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 1000;
    public int health;

    public Slider healthSlider;

    void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(int dmg) {
        Debug.Log("Damage Taken: " + dmg);
        
        health -= dmg;
        healthSlider.value = (float)health / (float)maxHealth;
    }
}
