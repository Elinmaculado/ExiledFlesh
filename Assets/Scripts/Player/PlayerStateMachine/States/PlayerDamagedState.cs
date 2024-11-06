using UnityEngine;

public class PlayerDamagedState : PlayerState
{
    public PlayerDamagedState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
       controller.SetAnimation("TakeDamage");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}