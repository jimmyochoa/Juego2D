using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;

    private Image heartImage;
    private int currentLife = 3; // Inicialmente se asume que el jugador tiene 3 vidas

    void Start()
    {
        heartImage = GetComponent<Image>();
        UpdateHeartSprite();
    }

    // Método para actualizar el sprite del corazón
    void UpdateHeartSprite()
    {
        switch (currentLife)
        {
            case 3:
                heartImage.sprite = fullHeartSprite;
                break;
            case 2:
                heartImage.sprite = halfHeartSprite;
                break;
            case 1:
                heartImage.sprite = emptyHeartSprite;
                break;
            default:
                break;
        }
    }

    // Método para disminuir una vida
    public void DecreaseLife()
    {
        currentLife--;
        if (currentLife < 0)
        {
            currentLife = 0;
        }
        UpdateHeartSprite();
    }

    // Método para aumentar una vida
    public void IncreaseLife()
    {
        currentLife++;
        if (currentLife > 3)
        {
            currentLife = 3;
        }
        UpdateHeartSprite();
    }
}
