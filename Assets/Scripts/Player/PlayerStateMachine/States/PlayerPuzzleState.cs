public class PlayerPuzzleState : PlayerState
{
    public PlayerPuzzleState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public override void Enter()
    {
        base.Enter();
        controller.SetPlayerVisibility(false);
    }

    public override void Exit()
    {
        base.Exit();
        controller.SetPlayerVisibility(true);
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