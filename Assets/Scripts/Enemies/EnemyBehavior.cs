using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float detectionRange;
    public float hitCooldown = 2f;
    public int damage = 1;

    private GameObject player;
    private Transform playerPosition;
    private bool isWaiting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
    }

    void Update()
    {
        if (player != null && !isWaiting)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Calcula la dirección hacia el jugador en el plano XZ
                Vector3 direction = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;

                // Calcula la nueva posición solo en X y Z
                Vector3 newPosition = new Vector3(transform.position.x + direction.x * moveSpeed * Time.deltaTime,
                                                   transform.position.y, // Mantiene la posición Y
                                                   transform.position.z + direction.z * moveSpeed * Time.deltaTime);

                // Asigna la nueva posición
                transform.position = newPosition;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hubo una colision");
        if (collision.gameObject.CompareTag("Player"))
        {
            isWaiting = true;
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                StartCoroutine(WaitAfterHit());
            }
        }
    }

    IEnumerator WaitAfterHit()
    {
        // isWaiting = true;
        yield return new WaitForSeconds(hitCooldown); 
        isWaiting = false;
    }
}
