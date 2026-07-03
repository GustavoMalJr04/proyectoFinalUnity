using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ¡Importante para usar el componente Image de los corazones!
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    [Header("Componentes UI")]
    public TextMeshProUGUI scoreText; 
    public Player playerScript;       
    
    // Aquí arrastra tus 3 imágenes de corazones en el Inspector
    public Image[] corazonesUI; 

    private int currentScore = 0;

    void Awake()
    {
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
        // Forzamos a que al arrancar el nivel, los 3 corazones estén encendidos
        if (corazonesUI != null)
        {
            for (int i = 0; i < corazonesUI.Length; i++)
            {
                if (corazonesUI[i] != null)
                {
                    corazonesUI[i].enabled = true;
                }
            }
        }

        UpdateScoreText();
    }

    // Sumar puntos al recolectar Gemas
    public void AddPoints(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        UpdateScoreText();
    }

    // Refrescar pantalla cuando el jugador pierde vida
    public void RefreshUI()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // 1. Gestionar el formato del Score a 000
        if (scoreText != null)
        {
            // "D3" transforma el número para que siempre muestre mínimo 3 dígitos (ej: 005)
            scoreText.text = currentScore.ToString("D3");
        }

        // 2. Gestionar el encendido/apagado de los corazones visuales
        if (playerScript != null && corazonesUI != null)
        {
            for (int i = 0; i < corazonesUI.Length; i++)
            {
                // Como pusiste 'public int currentLives' en tu Player, podemos leerlo directo:
                if (i < playerScript.currentLives)
                {
                    corazonesUI[i].enabled = true;  // Muestra el corazón
                }
                else
                {
                    corazonesUI[i].enabled = false; // Apaga el corazón si ya perdió esa vida
                }
            }
        }
    }
}