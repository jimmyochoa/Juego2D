using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerJumpForce = 20f;
    public float playerSpeed = 10f;
    public float jumpAngle = 45f; // Ángulo de salto ajustable
    public float animationDuration = 0.5f;
    public Sprite[] walkingSprites;

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myCollider;
    private bool isGrounded = false; // Variable para verificar si el jugador está en el suelo
    private bool canJump = true; // Variable para controlar si el jugador puede saltar

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(ChangeSprite());
    }

    void Update()
    {
        // Verificar si el jugador puede saltar (máximo dos saltos)
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || canJump))
        {
            Jump();
        }

        // Siempre camina
        myRigidbody.velocity = new Vector2(playerSpeed, myRigidbody.velocity.y);
    }

    void Jump()
    {
        // Si está en el suelo o puede saltar, realizar el salto
        if (isGrounded || canJump)
        {
            // Calculamos las componentes horizontal y vertical de la fuerza del salto
            float jumpAngleInRadians = Mathf.Deg2Rad * jumpAngle; // Convertimos el ángulo a radianes
            float jumpForceX = playerJumpForce * Mathf.Cos(jumpAngleInRadians);
            float jumpForceY = playerJumpForce * Mathf.Sin(jumpAngleInRadians);

            // Aplicamos la fuerza del salto al Rigidbody
            myRigidbody.velocity = new Vector2(jumpForceX, jumpForceY);

            // Si está en el suelo, permitir un salto adicional
            if (isGrounded)
            {
                canJump = true;
            }
            else
            {
                // Si no está en el suelo, indicar que ya no puede saltar adicionalmente
                canJump = false;
            }
        }
    }

    IEnumerator ChangeSprite()
    {
        while (true)
        {
            yield return new WaitForSeconds(animationDuration);
            mySpriteRenderer.sprite = walkingSprites[Random.Range(0, walkingSprites.Length)];
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
