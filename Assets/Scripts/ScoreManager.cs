using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton para llamarlo desde Player

    public TextMeshProUGUI scoreText; // Arrastra aquí tu objeto ScoreText
    public Player playerScript;       // Arrastra aquí tu objeto Player

    private int currentScore = 0;

    void Awake()
    {
        // Configuramos el Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // Función pública para sumar puntos
    public void AddPoints(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        UpdateScoreText();
    }

    // Función pública para refrescar la pantalla cuando el jugador pierde vida
    public void RefreshUI()
    {
        UpdateScoreText();
    }

    // Traduce los datos a la pantalla
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            int vidasActuales = (playerScript != null) ? playerScript.maxLives : 3; 
            // Si el juego ya empezó, intentamos leer las vidas reales del script
            // Nota: Como en tu script 'currentLives' es private, usamos una aproximación o puedes cambiar 'private int currentLives' a 'public int currentLives' en tu Player.cs para que se lea exacto.
            
            scoreText.text = "Score: " + currentScore;
        }
    }
}