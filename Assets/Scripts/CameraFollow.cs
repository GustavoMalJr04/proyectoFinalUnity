using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //referencia al objeto que queremos seguir (el jugador)
    public float smoothness = 0.125f; //velocidad de seguimiento de la cámara
    public Vector3 offset = new Vector3(0f, 0f, -10f); //desplazamiento de la cámara respecto al jugador

    void LateUpdate()
    {
        if (target != null) //si el objeto a seguir no es nulo
        {
            Vector3 desiredPosition = target.position + offset; //calculamos la posición deseada de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness); //interpolamos entre la posición actual de la cámara y la posición deseada
            transform.position = smoothedPosition; //asignamos la posición suavizada a la cámara
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
