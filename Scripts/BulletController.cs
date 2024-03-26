// BulletController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public float bulletSpeed = 2000f;
    public float maxDistance = 2000f; // Distancia máxima que la bala puede viajar
    private Vector2 initialPosition; // Posición inicial de la bala
    public GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // Guardamos la posición inicial
    }

    void Update()
    {
        float step = bulletSpeed * Time.deltaTime; // Calcula la distancia que la bala debe moverse en este frame.
        transform.position += transform.right * step; // Mueve la bala hacia adelante.

        // Comprobación de la distancia máxima como antes.
        if (Vector2.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Destruir la bala si ha alcanzado la distancia máxima.
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ItemGood")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "ItemBad")
        {
            // SE DESTRUYE AL OBJETO, ADEMAS AGREGAMOS SCORE Y VIDAS
            Destroy(collision.gameObject);
            gameManager.IncreaseScore();
            gameManager.IncreaseLives();
        }
    }
}
