using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text textScore;

    public int maxHealth = 100;

    public int currentHealth = 100;
    public HealthBar healthBar;

    public GameObject relicPrefab; // Asigna el prefab de la reliquia en el inspector
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // Ajusta a donde quieras que aparezca la reliquia


    void Start()
    {
        score = 0;
        textScore.text = "Score: " + score;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        StartCoroutine(GameTimer());

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void IncreaseScore()
    {
        score++;
        textScore.text = "Score: " + score;
    }
    public void IncreaseLives()
    {
        currentHealth += 10;
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(60); // Espera 35 segundos
    }
}