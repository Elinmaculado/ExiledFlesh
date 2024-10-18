using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de avance y retroceso
    public float velocidadRotacion = 100f; // Velocidad de rotación 

    private void Update()
    {
        // Movimiento hacia adelante y atrás 
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(-Vector3.forward * (velocidadMovimiento/2) * Time.deltaTime);
        }

        // Rotación a la izquierda y derecha
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -velocidadRotacion * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
    }
}
