using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // Aseg�rate de importar Cinemachine al principio

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public float blockCooldown;
    private int currentHealth;
    private Rigidbody rb;
    private bool canBlock = true;

    // Referencia al componente CinemachineImpulseSource
    [SerializeField] private CinemachineImpulseSource impulseSource;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        if (Input.GetKey(KeyCode.E) && canBlock)
        {
            Debug.Log("Jugador bloque� el da�o.");
            canBlock = false;
            Invoke("ActivateBlock", blockCooldown);
        }
        else
        {
            currentHealth -= damage;
            Debug.Log("Player health: " + currentHealth);

            // Genera el impulso para sacudir la c�mara
            if(impulseSource != null){impulseSource.GenerateImpulse();}
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void PushBack(Vector3 direction, float force)
    {
        if (rb != null)
        {
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    void ActivateBlock()
    {
        canBlock = true;
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }
}