using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab de la plataforma que deseas generar
    public float generationInterval = 2f; // Intervalo entre generaciones
    public float platformOffset = 10f; // Distancia entre la posición del jugador y la generación de la plataforma

    private Transform playerTransform;
    private float lastGenerationTime;
    private List<GameObject> platforms = new List<GameObject>(); // Lista para almacenar las plataformas generadas
    private Vector3 lastPlayerPosition; // Última posición conocida del jugador

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
        lastPlayerPosition = playerTransform.position;
        lastGenerationTime = Time.time;
    }

    void Update()
    {
        // Verificar si el jugador se está moviendo
        if (playerTransform.position == lastPlayerPosition)
        {
            return; // No generar plataformas si el jugador no se está moviendo
        }
        else
        {
            lastPlayerPosition = playerTransform.position;
        }

        if (Time.time - lastGenerationTime > generationInterval)
        {
            GeneratePlatform();
            lastGenerationTime = Time.time;
        }

        // Eliminar las plataformas que están fuera de la vista de la cámara
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i] == null || platforms[i].transform.position.x < playerTransform.position.x - platformOffset)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
                i--; // Necesario para evitar errores de índice fuera de rango
            }
        }
    }

    void GeneratePlatform()
    {
        Vector3 spawnPosition = new Vector3(playerTransform.position.x + platformOffset, transform.position.y, transform.position.z);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        
        // Mueve la plataforma al mismo nivel que el generador
        newPlatform.transform.position = new Vector3(newPlatform.transform.position.x, transform.position.y, transform.position.z);

        platforms.Add(newPlatform); // Agrega la nueva plataforma a la lista
    }
}
