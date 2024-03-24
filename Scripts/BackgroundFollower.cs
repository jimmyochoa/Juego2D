using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    [SerializeField] private Vector2 movementSpeed;
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(playerRigidbody != null) {
            Vector2 offset = playerRigidbody.velocity * movementSpeed * Time.deltaTime;

            foreach(Transform child in transform) {
                Renderer renderer = child.GetComponent<Renderer>();
                if(renderer != null) {
                    Material material = renderer.material;
                    material.mainTextureOffset += offset;
                }
            }
        }
    }
}
