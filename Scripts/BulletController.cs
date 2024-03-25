// BulletController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public float bulletSpeed = 10f;
    public float maxDistance = 10f; // Distancia máxima que la bala puede viajar
    private Vector2 initialPosition; // Posición inicial de la bala
    public GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // Guardamos la posición inicial
    }

    void Update()
    {
        // Movemos la bala en la dirección especificada
        myRigidbody2D.velocity = transform.right * bulletSpeed;

        // Comprobamos si la bala ha viajado más allá de la distancia máxima
        if (Vector2.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Destruir la bala si ha alcanzado la distancia máxima
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
