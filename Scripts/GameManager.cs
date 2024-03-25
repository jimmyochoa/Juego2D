using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text textScore;

    public int maxHealth = 100;

    public int currentHealth = 100;
    public HealthBar healthBar;

    void Start()
    {
        score = 0;
        textScore.text = "Score: " + score;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void IncreaseScore()
    {
        score++;
        textScore.text = "Score: " + score;
    }
}
