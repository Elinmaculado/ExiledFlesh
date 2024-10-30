using UnityEngine;
public class IdleState : EnemyState
{
    public IdleState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller){}

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
        Debug.Log("Idle");
        controller.player = GameObject.FindGameObjectWithTag("Player");
        if (controller.player != null){ 
            stateMachine.ChangeState(controller.chasingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}