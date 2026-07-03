using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f; //velocidad del jugador
    private Rigidbody2D rb2D; //referencia al componente Rigidbody2D del jugador
    private float move;
    private SpriteRenderer spriteRenderer;//referencia al componente SpriteRenderer del jugador
    private Animator anim; //referencia al componente Animator del jugador
    public float jumpForce = 4f; //fuerza del salto
    private bool isGrounded; //variable para saber si el jugador está en el suelo
    public Transform groundCheck; //referencia al objeto que se usa para comprobar si el jugador está en el suelo
    public float groundRadius = 0.2f; //radio del círculo que se usa para comprobar si el jugador está en el suelo
    public LayerMask groundLayer; //capa que se usa para comprobar si el jugador está en el suelo

    public int maxLives=3;
    private int currentLives;
    private bool isDead = false;

    void Start() //cuando se da play
    {
        rb2D = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        currentLives = maxLives;//inicia con 3 vidas
    }

    void Update()
    {

        if (isDead) return; //si el jugador está muerto, no hacemos nada

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer); //comprobamos si el jugador está en el suelo

        move = Input.GetAxisRaw("Horizontal"); //obtenemos el input del jugador, se mueve con A,D y flechas izquierda y derecha
        rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y); //movemos al jugador en el eje x

        if(move !=0)
        {
            spriteRenderer.flipX = move < 0; //giramos el sprite del jugador para que mire hacia la dirección en la que se mueve
        }

        anim.SetBool("isWalking", move != 0); //si el jugador se mueve, activamos la animación de caminar
        anim.SetBool("isGrounded", isGrounded); //si el jugador está en el suelo, activamos la animación de estar en el suelo

        bool presionandoAbajo = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); //comprobamos si el jugador está presionando la tecla de abajo
        anim.SetBool("isCrouching", presionandoAbajo && isGrounded); //si el jugador está agachado y en el suelo

        if (Input.GetButtonDown("Jump") && isGrounded) //si el jugador pulsa la tecla de salto y está en el suelo
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //aplicamos una fuerza hacia arriba para que el jugador salte
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //cuando el jugador colisiona con otro objeto
    {
        if (isDead) return; //si el jugador está muerto, no hacemos nada

        if (collision.gameObject.CompareTag("Enemy")) //si el jugador colisiona con un enemigo
        {
            TakeDamage(); //el jugador recibe daño
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WinZone"))
        {
            WinGame(); //si el jugador entra en la zona de victoria, ganamos el juego
        }

    }

    void TakeDamage()
    {
        currentLives--;
        Debug.Log("Vidas restantes: " + currentLives);

        if(currentLives > 0)
        {
            anim.SetTrigger("hurt"); //activamos la animación de recibir daño

            float pushDirection = spriteRenderer.flipX ? 1f : -1f; //determinamos la dirección de empuje basada en la orientación del sprite
            rb2D.velocity = Vector2.zero; //reseteamos la velocidad del jugador
            rb2D.AddForce(new Vector2(pushDirection * 5f, 5f), ForceMode2D.Impulse); //empujamos al jugador hacia atrás y hacia arriba

        }
        else
        {
            Die(); //si el jugador no tiene vidas, muere
        }
    }

    void Die()
    {
        isDead = true;
        rb2D.velocity = Vector2.zero; //reseteamos la velocidad del jugador
        anim.SetTrigger("die"); //activamos la animación de morir
        Debug.Log("¡Has muerto!");

        Invoke("RestartLevel", 2f); //reiniciamos el nivel después de 2 segundos
    }

    void WinGame()
    {
        isDead = true; //desactivamos el control del jugador
        rb2D.velocity = Vector2.zero; //reseteamos la velocidad del jugador
        anim.SetTrigger("win"); //activamos la animación de ganar
        Debug.Log("¡Has ganado!");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reiniciamos el nivel actual
    }
    
    private void FixedUpdate() //se ejecuta a una velocidad constante, ideal para físicas
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer); //comprobamos si el jugador está en el suelo
    }
}
