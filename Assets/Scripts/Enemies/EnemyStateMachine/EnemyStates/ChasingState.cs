using UnityEngine;
public class ChasingState : EnemyState
{
    public ChasingState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller){}

    public override void Enter()
    {
        base.Enter();
        controller.player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Debug.Log("Chase");
        controller.player = GameObject.FindGameObjectWithTag("Player");
        if (controller.player == null){ 
            stateMachine.ChangeState(controller.idleState);
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(controller.transform.position, controller.player.transform.position);

        if (distanceToPlayer <= controller.detectionRange)
        {
            // Calcula la direcci�n hacia el jugador en el plano XZ
            Vector3 direction = new Vector3(controller.player.transform.position.x -controller.transform.position.x, 0, controller.player.transform.position.z - controller.transform.position.z).normalized;

            // Calcula la nueva posici�n solo en X y Z
            Vector3 newPosition = new Vector3(controller.transform.position.x + direction.x * controller.moveSpeed * Time.deltaTime,
                                                controller.transform.position.y, // Mantiene la posici�n Y
                                                controller.transform.position.z + direction.z * controller.moveSpeed * Time.deltaTime);

            // Asigna la nueva posici�n
            controller.transform.position = newPosition;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}