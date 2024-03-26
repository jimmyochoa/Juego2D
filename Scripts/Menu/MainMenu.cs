using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool started = false; // Static para que la variable sea compartida entre todas las instancias del script

    void Start()
    {
        // Cargar la escena del menú principal solo si el juego aún no ha comenzado
        if (!started)
        {
            SceneManager.LoadScene("MainMenuScene");
            started = true; // Establecer el juego como iniciado
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
