using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public float blockCooldown;
    private int currentHealth;
    private Rigidbody rb;
    private bool canBlock = true;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        if (Input.GetKey(KeyCode.E) && canBlock)
        {
            Debug.Log("Jugador bloqueó el daño.");
            canBlock = false;
            Invoke("ActivateBlock", blockCooldown);
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
