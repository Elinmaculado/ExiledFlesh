using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class IdleState : EnemyState
{
    private Vector3 wanderTarget;
    private bool isWandering = false;
    // Tiempo de espera antes de cambiar de punto
    private float waitTime = 3;
    private float wonderDistance = 5;

    public IdleState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller) { }

    public override void Enter()
    {
        base.Enter();
        controller.navMeshAgent.isStopped = false;

        if (!isWandering)
        {
            StartWandering();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopWandering();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (controller.player != null)
        {
            float distanceToPlayer = Vector3.Distance(controller.transform.position, controller.player.transform.position);
            if (distanceToPlayer <= controller.detectionRange)
            {
                StopWandering();
                stateMachine.ChangeState(controller.chasingState);
                return;
            }
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

        // Dejar al enemigo en su posici�n actual
        controller.navMeshAgent.SetDestination(controller.transform.position);
    }

    // Rutina de vagabundeo
    private IEnumerator Wander()
    {
        while (isWandering)
        {
            // Seleccionar un nuevo punto solo si ha llegado a su destino actual
            if (!controller.navMeshAgent.pathPending && controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
            {
                wanderTarget = GetRandomPointInNavMesh();
                controller.navMeshAgent.SetDestination(wanderTarget);
                yield return new WaitForSeconds(waitTime); // Esperar antes de buscar un nuevo punto
            }
            yield return null;
        }
    }

    // M�todo para obtener un punto aleatorio en el NavMesh
    private Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wonderDistance;
        randomDirection += controller.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // Si no se encuentra una posici�n v�lida, devuelve la posici�n actual
        return controller.transform.position;
    }
}
