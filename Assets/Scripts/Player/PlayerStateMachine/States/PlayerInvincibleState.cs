using UnityEngine;
public class PlayerInvincibleState : PlayerState
{
    public PlayerInvincibleState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
        controller.rb.velocity = Vector3.zero;
        controller.SetAnimation("Idle");
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