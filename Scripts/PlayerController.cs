// PlayerController.cs
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
    public GameManager gameManager;

    private SpriteRenderer mySpriteRenderer;
    private Rigidbody2D myRigidbody;
    private bool isGrounded = false;
    private bool canJump = true;
    private bool gameOver = false;
    private Vector3 initialPosition; // Posición inicial del jugador
    public GameObject bulletPrefab;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeSprite());
        gameOverText.enabled = false;

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

            // Movimiento del jugador
            if (!Input.GetKey(KeyCode.F))
            {
                myRigidbody.velocity = new Vector2(playerSpeed, myRigidbody.velocity.y);
            }
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

        // Disparo de la bala
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootBullet();
        }
    }

    void Jump()
    {
        // Cálculo de la fuerza de salto
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
            // Se resta toda la vida
            gameManager.TakeDamage(gameManager.currentHealth);
            GameOver();
        }
        else if (collision.CompareTag("Money"))
        {
            gameManager.IncreaseScore();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("ItemBad"))
        {
            gameManager.TakeDamage(15);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("FinalItem"))
        {
            //IR A NIVEL 2
            SceneManager.LoadScene("Level2");
        }
        
    }

    void GameOver()
    {
        gameOver = true;
        gameOverText.enabled = true;
        myRigidbody.velocity = Vector2.zero; // Detiene el movimiento del jugador cuando ocurre el Game Over
    }

    void ShootBullet()
    {
        // Asegúrate de que el prefab de la bala esté asignado
        if (bulletPrefab != null)
        {
            // Instanciamos la bala
            GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Aquí podrías realizar cualquier otra configuración necesaria para la bala, como asignar velocidad, etc.
        }
        else
        {
            Debug.LogError("Bullet prefab not assigned!");
        }
    }
}
