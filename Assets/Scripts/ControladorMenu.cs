using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ControladorMenu : MonoBehaviour
{
    // Función pública que llamará el botón Play
    public void Jugar()
    {
        // Carga la escena del juego (la número 1 en Build Settings)
        SceneManager.LoadScene(1); 
    }

    // Función opcional para un botón de salir
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}