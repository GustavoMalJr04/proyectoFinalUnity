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
    // Si va a la derecha y pasa de la posición X = 5, da la vuelta
    if (movingRight && transform.position.x > 5f)
    {
        movingRight = false;
    }
    // Si va a la izquierda y regresa a la posición X = -2, da la vuelta
    else if (!movingRight && transform.position.x < -2f)
    {
        movingRight = true;
    }

    // Código de movimiento que ya tenías
    if (movingRight)
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    else
    {
        rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    }    
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Floor")) 
        {
            movingRight = !movingRight; 
        }  
    }
    
}
