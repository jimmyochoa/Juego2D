using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de incluir esto

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    public void Pausa()
    {
        Time.timeScale = 0f; // Pausa el tiempo del juego
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego.
        gameObject.SetActive(false); // Oculta el menú de pausa.
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenuScene"); 
    }

}
