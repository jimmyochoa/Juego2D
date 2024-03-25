using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerJumpForce = 20f;
    public float playerSpeed = 10f;
    public float jumpAngle = 45f;
    public float animationDuration = 0.5f;
    public Sprite[] walkingSprites;
    public Text gameOverText;
    public GameManager GameManager;
    public GameObject[] heartSprites; // Arreglo de GameObjects para los sprites de corazón

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody;
    private bool isGrounded = false;
    private bool canJump = true;
    private bool gameOver = false;
    private int lives = 3; // Número de vidas inicial

    public Sprite fullHeartSprite; // Sprite para la vida completa
    public Sprite halfHeartSprite; // Sprite para la vida a la mitad
    public Sprite emptyHeartSprite; // Sprite para la vida vacía

    private Vector3 initialPosition; // Posición inicial del jugador

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeSprite());
        gameOverText.enabled = false;
        UpdateHeartSprites(); // Actualiza los sprites de los corazones

        // Guardar la posición inicial del jugador
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || canJump))
            {
                Jump();
            }
            myRigidbody.velocity = new Vector2(playerSpeed, myRigidbody.velocity.y);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        // Recargar la escena si se presiona 'e'
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Jump()
    {
        float jumpAngleInRadians = Mathf.Deg2Rad * jumpAngle;
        float jumpForceX = playerJumpForce * Mathf.Cos(jumpAngleInRadians);
        float jumpForceY = playerJumpForce * Mathf.Sin(jumpAngleInRadians);
        myRigidbody.velocity = new Vector2(jumpForceX, jumpForceY);
        if (isGrounded)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            if (lives > 0)
            {
                LoseLife();
                Debug.Log("Vidas restantes: " + lives);
                // Reiniciar la posición del jugador
                transform.position = initialPosition;
            }
            else
            {
                GameOver();
            }
        }
        else if (collision.CompareTag("Money"))
        {
            GameManager.IncreaseScore();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("ItemBad"))
        {
            if (lives > 0)
            {
                LoseLife();
            }
            else
            {
                GameOver();
            }
            Destroy(collision.gameObject);
        }
    }

    void LoseLife()
    {
        lives--;
        Debug.Log("Perdió una vida");
        UpdateHeartSprites(); // Actualiza los sprites de los corazones
    }

    void UpdateHeartSprites()
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            // Asigna los sprites adecuados según la cantidad de vidas restantes
            if (i >= lives) // Vida vacía
            {
                heartSprites[i].GetComponent<SpriteRenderer>().sprite = emptyHeartSprite;
            }
            else if (i == lives - 1 && lives % 2 == 0) // Vida a la mitad
            {
                heartSprites[i].GetComponent<SpriteRenderer>().sprite = halfHeartSprite;
            }
            else // Vida completa
            {
                heartSprites[i].GetComponent<SpriteRenderer>().sprite = fullHeartSprite;
            }
        }
    }

    void GameOver()
    {
        gameOver = true;
        gameOverText.enabled = true;
        myRigidbody.velocity = Vector2.zero; // Detiene el movimiento del jugador cuando ocurre el Game Over
    }
}
