using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Jugador bloque� el da�o.");
            // El jugador no recibe da�o, pero el enemigo a�n espera unos segundos
        }
        else
        {
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }
}
