using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // Aseg�rate de importar Cinemachine al principio

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 2;
    public float blockCooldown;
    [SerializeField] private float currentHealth;
    public float healAmount;
    private Rigidbody rb;
    private bool canBlock = true;

    [SerializeField] PlayerController controller;

    // Referencia al componente CinemachineImpulseSource
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] LifeScreen lifeScreen;

    void Start()
    {
        currentHealth = maxHealth;
        lifeScreen.SetAlpha(1-(currentHealth/maxHealth));
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(currentHealth<=maxHealth){
            currentHealth += healAmount * Time.deltaTime;
            lifeScreen.SetAlpha(1-(currentHealth/maxHealth));
        }
    }

    public void TakeDamage(int damage, float stunDuration = 1.0f)
    {
        if (Input.GetKey(KeyCode.E) && canBlock)
        {
            Debug.Log("Jugador bloque� el da�o.");
            canBlock = false;
            Invoke("ActivateBlock", blockCooldown);
        }
        else
        {
            controller.EnterDamage(stunDuration);
            currentHealth -= damage;
            Debug.Log("Player health: " + currentHealth);
            lifeScreen.SetAlpha(currentHealth/maxHealth);

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
            direction.y = 0;
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