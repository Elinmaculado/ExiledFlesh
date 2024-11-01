using UnityEngine;
public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // Movimiento hacia adelante y atrás    
         if (Input.GetKey(KeyCode.W))
        {
            controller.transform.Translate(Vector3.forward * controller.currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            controller.transform.Translate(-Vector3.forward * (controller.currentSpeed/2) * Time.deltaTime);
        }

        // Rotación a la izquierda y derecha
        if (Input.GetKey(KeyCode.A))
        {
            controller.transform.Rotate(Vector3.up, -controller.rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.transform.Rotate(Vector3.up, controller.rotationSpeed * Time.deltaTime);
        }

                //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.currentSpeed = controller.sprintSpeed;
        }
        else
        {
            controller.currentSpeed = controller.walkingSpeed;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}