using UnityEngine;

public class PlayerHiddingState : PlayerState
{
    public PlayerHiddingState(PlayerStateMachine stateMachine, PlayerController controller) : base(stateMachine, controller)
    {
    }

    Vector3 startPoint;
    Vector3 hiddePoint;

    public override void Enter(){
        base.Enter();
        controller.rb.isKinematic = true;
        controller.gameObject.transform.position = startPoint;
        controller.SetMeshState(false);
        controller.PlayerHiddenState.exitPoint = startPoint;
    }

    public override void Exit()
    {
        base.Exit();
        controller.transform.localScale = Vector3.one * 0.25f;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        controller.gameObject.transform.position = Vector3.Lerp(controller.gameObject.transform.position, hiddePoint,10 * Time.deltaTime);
        if(HasArrived()){
            stateMachine.ChangeState(controller.PlayerHiddenState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetPoints(Vector3 _startPoint, Vector3 _hiddenPoint){
        startPoint = _startPoint;
        hiddePoint = _hiddenPoint;
    }

    bool HasArrived(){
        return (controller.gameObject.transform.position.x < hiddePoint.x + 0.05 && controller.gameObject.transform.position.x > hiddePoint.x - 0.05)
             && (controller.gameObject.transform.position.z < hiddePoint.z + 0.05 && controller.gameObject.transform.position.z > hiddePoint.z - 0.05);
    }


}