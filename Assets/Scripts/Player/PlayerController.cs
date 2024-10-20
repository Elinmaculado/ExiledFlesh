using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed; // Velocidad de avance y retroceso
    public float velocidadRotacion = 100f; // Velocidad de rotación 
    public float walkingSpeed = 5f;
    public float sprintSpeed = 8f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
        }

        // Movimiento hacia adelante y atrás 
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(-Vector3.forward * (currentSpeed/2) * Time.deltaTime);
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
