using UnityEngine;
using System.Collections.Generic;

public class Camera2D : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject[] backgroundContainers; // Arreglo de contenedores de fondo
    public float parallaxMultiplier = 0.5f; // Multiplicador de efecto de parallax

    private Vector3 previousCameraPosition;
    private float cameraWidth;
    private List<GameObject> backgroundObjects = new List<GameObject>();

    void Start()
    {
        previousCameraPosition = transform.position;
        cameraWidth = Camera.main.aspect * Camera.main.orthographicSize;

        // Obtener los objetos de fondo de cada contenedor
        foreach (GameObject container in backgroundContainers)
        {
            if (container != null)
            {
                foreach (Transform child in container.transform)
                {
                    backgroundObjects.Add(child.gameObject);
                }
            }
        }
    }

    void LateUpdate()
    {
        if (targetPlayer != null && backgroundObjects != null)
        {
            // Calculamos el desplazamiento de la cámara desde el cuadro anterior
            float deltaX = targetPlayer.position.x - previousCameraPosition.x;

            // Movemos la cámara
            transform.position = new Vector3(targetPlayer.position.x, transform.position.y, transform.position.z);

            // Movemos los objetos del fondo junto con la cámara
            foreach (GameObject backgroundObject in backgroundObjects)
            {
                MoveBackgroundObject(backgroundObject, deltaX);
            }

            // Actualizamos la posición anterior de la cámara
            previousCameraPosition = transform.position;
        }
    }

    void MoveBackgroundObject(GameObject backgroundObject, float deltaX)
    {
        // Calculamos el desplazamiento para este objeto del fondo
        float backgroundDeltaX = deltaX * parallaxMultiplier;

        // Movemos el objeto del fondo en función del desplazamiento
        backgroundObject.transform.Translate(Vector3.right * backgroundDeltaX);

        // Si el objeto del fondo ha salido completamente de la vista de la cámara,
        // lo movemos de nuevo al otro extremo para que aparezca en el lado opuesto
        if (Mathf.Abs(backgroundObject.transform.position.x - transform.position.x) >= cameraWidth + backgroundObject.GetComponent<SpriteRenderer>().bounds.size.x / 2)
        {
            float backgroundNewX = backgroundObject.transform.position.x - Mathf.Sign(deltaX) * (cameraWidth + backgroundObject.GetComponent<SpriteRenderer>().bounds.size.x);
            backgroundObject.transform.position = new Vector3(backgroundNewX, backgroundObject.transform.position.y, backgroundObject.transform.position.z);
        }
    }
}
