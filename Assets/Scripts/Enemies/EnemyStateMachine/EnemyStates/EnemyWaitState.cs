using UnityEngine;
public class EnemyWaitState : EnemyState
{
    public EnemyWaitState(EnemyStateMachine stateMachine, EnemyController controller) : base(stateMachine, controller)
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
        Debug.Log("wait");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}