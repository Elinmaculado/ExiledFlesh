using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class IdleState : EnemyState
{
    private Vector3 wanderTarget;
    private bool isWandering = false;
    private float waitTime = 5f; // Tiempo de espera entre movimientos

    public IdleState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        Debug.Log("Entered idle state");
        base.Enter();
        StartWandering();
    }

    public override void Exit()
    {
        base.Exit();
        StopWandering();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //Debug.Log("Idle");

        // Revisar si el jugador está dentro del rango
        controller.player = GameObject.FindGameObjectWithTag("Player");
        if (controller.player != null)
        {
            stateMachine.ChangeState(controller.chasingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void StartWandering()
    {
        isWandering = true;
        controller.StartCoroutine(Wander());
    }

    private void StopWandering()
    {
        isWandering = false;
        controller.StopCoroutine(Wander());
        controller.navMeshAgent.SetDestination(controller.transform.position);
    }

    private IEnumerator Wander()
    {
        while (isWandering)
        {
            // Seleccionar un nuevo punto aleatorio dentro del NavMesh solo si está cerca del destino actual
            if (!controller.navMeshAgent.pathPending && controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
            {
                // Seleccionar un nuevo punto aleatorio en el NavMesh
                wanderTarget = GetRandomPointInNavMesh();
                controller.navMeshAgent.SetDestination(wanderTarget);

                // Esperar el tiempo antes de elegir un nuevo punto
                yield return new WaitForSeconds(waitTime);
            }

            // Esperar un pequeño intervalo para no sobrecargar el ciclo
            yield return null;
        }
    }


    private Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 5f;
        randomDirection += controller.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return controller.transform.position;
    }
}
