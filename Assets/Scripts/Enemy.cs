using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; //velocidad del enemigo
    private bool movingRight = true; //variable para saber si el enemigo se mueve hacia la derecha
    private Rigidbody2D rb2D; //referencia al componente Rigidbody2D del enemigo

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); //obtenemos la referencia al componente Rigidbody2D
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y); //si el enemigo se mueve hacia la derecha, le damos una velocidad positiva en el eje x
            transform.localScale = new Vector3(-1f, 1f, 1f); //giramos el sprite del enemigo para que mire hacia la derecha
        }
        else
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y); //si el enemigo se mueve hacia la izquierda, le damos una velocidad negativa en el eje x
            transform.localScale = new Vector3(1f, 1f, 1f); //giramos el sprite del enemigo para que mire hacia la izquierda
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) //si el enemigo colisiona con un objeto que no sea el jugador
        {
            movingRight = !movingRight; //cambiamos la dirección del movimiento del enemigo
        }  
    }   
}
