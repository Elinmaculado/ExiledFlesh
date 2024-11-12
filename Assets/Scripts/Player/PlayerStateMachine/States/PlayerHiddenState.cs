using UnityEngine;
public class PlayerHiddenState : PlayerState
{
    public PlayerHiddenState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    public Vector3 exitPoint;

    public override void Enter()
    {
        base.Enter();
        controller.tag = "HiddenPlayer";
        controller.SetAnimation("Idle");
        Debug.Log(controller.gameObject.name);
        Debug.Log(controller.tag);
    }

    public override void Exit()
    {
        base.Exit();
        controller.transform.localScale = Vector3.one;
        controller.transform.position = exitPoint;
        controller.tag = "Player";
        controller.rb.isKinematic = false;
        controller.SetMeshState(true);
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