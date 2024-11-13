using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    float moveInput;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        controller.rb.velocity = Vector3.zero;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // Movimiento hacia adelante y atrás  
        moveInput = Input.GetAxisRaw("Vertical") * controller.currentSpeed;
        moveInput = Mathf.Clamp(moveInput,-controller.currentSpeed/2,controller.currentSpeed);

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
        if(moveInput !=0){
            
            if(moveInput>0){controller.SetAnimation("Walk");}
            else{controller.SetAnimation("WalkBack");}

            controller.rb.velocity = moveInput * controller.transform.forward;
            
        }
        else{
            controller.SetAnimation("Idle");
            controller.rb.velocity = new Vector3(0,controller.rb.velocity.y,0);
        }
    }
}