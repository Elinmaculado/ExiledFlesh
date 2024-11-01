using UnityEngine;
public class PlayerHiddenState : PlayerState
{
    public PlayerHiddenState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
        controller.tag = "HiddenPlayer";
    }

    public override void Exit()
    {
        base.Exit();
        controller.tag = "Player";
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(Input.anyKeyDown){
            stateMachine.ChangeState(controller.PlayerIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}