using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGema : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto que entra en la gema tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Le sumamos 1 punto al Singleton ScoreManager
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddPoints(1);
            }

            // Destruimos la gema de la escena
            Destroy(gameObject);
        }
    }
}