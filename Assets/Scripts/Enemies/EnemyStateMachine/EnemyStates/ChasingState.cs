using UnityEngine;
using UnityEngine.AI;

public class ChasingState : EnemyState
{
    private NavMeshAgent navMeshAgent;

    public ChasingState(EnemyStateMachine stateMachine, EnemyController controller, NavMeshAgent navMeshAgent)
        : base(stateMachine, controller)
    {
        this.navMeshAgent = navMeshAgent;
    }

    public ChasingState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
        controller.player = GameObject.FindGameObjectWithTag("Player");

        if (controller.player != null)
        {
            float distanceToPlayer = Vector3.Distance(controller.transform.position, controller.player.transform.position);
            if (distanceToPlayer <= controller.detectionRange)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = controller.moveSpeed;
                navMeshAgent.SetDestination(controller.player.transform.position);
            }
            else
            {
                // Si está fuera del rango, cambiar al estado Idle
                stateMachine.ChangeState(controller.idleState);
            }
        }
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (controller.player == null)
        {
            stateMachine.ChangeState(controller.idleState);
            return;
        }

        float distanceToPlayer = Vector3.Distance(controller.transform.position, controller.player.transform.position);

        // Si el jugador está en rango, actualizar el destino
        if (distanceToPlayer <= controller.detectionRange)
        {
            navMeshAgent.SetDestination(controller.player.transform.position);
        }
        else
        {
            // Si el jugador sale del rango, detener al NavMeshAgent y cambiar a Idle
            navMeshAgent.isStopped = true;
            stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit chasing state");

        // Detener el NavMeshAgent cuando salga del estado Chasing
        navMeshAgent.isStopped = true;
    }
}
