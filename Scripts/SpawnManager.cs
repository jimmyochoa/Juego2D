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

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador
        lastGenerationTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastGenerationTime > generationInterval)
        {
            GeneratePlatform();
            lastGenerationTime = Time.time;
        }
    }

    void GeneratePlatform()
    {
        Vector3 spawnPosition = new Vector3(playerTransform.position.x + platformOffset, transform.position.y, transform.position.z);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        
        // Mueve la plataforma al mismo nivel que el generador
        newPlatform.transform.position = new Vector3(newPlatform.transform.position.x, transform.position.y, transform.position.z);
    }
}
