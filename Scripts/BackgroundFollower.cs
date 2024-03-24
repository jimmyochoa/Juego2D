using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    
    [SerializeField] private Vector2 velocityMovement;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        material= GetComponent<SpriteRenderer>().material;
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = (playerRigidbody.velocity * 0.1f) * velocityMovement * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
