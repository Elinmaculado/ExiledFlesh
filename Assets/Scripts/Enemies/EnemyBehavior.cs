using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float detectionRange;
    public float hitCooldown = 2f;
    public int damage = 1;
    public float pushForce;

    private GameObject player;
    private Transform playerPosition;
    private bool isWaiting = false;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null && !isWaiting)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Actualiza la posición del destino en cada frame para seguir al jugador
                navMeshAgent.destination = playerPosition.position;
            }
            else
            {
                // Detiene al enemigo si el jugador está fuera del rango de detección
                navMeshAgent.ResetPath();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWaiting = true;
            navMeshAgent.isStopped = true;  // Detener temporalmente el movimiento del NavMeshAgent

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Empujar al jugador
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                playerHealth.PushBack(pushDirection, pushForce);

                // Inicia la rutina de espera después del golpe
                StartCoroutine(WaitAfterHit());
            }
        }
    }

    IEnumerator WaitAfterHit()
    {
        yield return new WaitForSeconds(hitCooldown);
        isWaiting = false;
        navMeshAgent.isStopped = false;  // Reanudar el movimiento del NavMeshAgent
    }
}
