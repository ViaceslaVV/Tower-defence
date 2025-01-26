using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int health = 3; // Pradinis gyvybi? skai?ius
    public TextMeshProUGUI healthText; // UI elementas gyvybi? rodymui

    void Start()
    {
        UpdateHealthUI(); // Atnaujink gyvybi? UI prad?ioje
    }

    public void ReduceHealth(int amount)
    {
        health -= amount;
        if (health < 0) health = 0; // Neleisk gyvyb?ms b?ti ma?iau u? 0
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }
}
