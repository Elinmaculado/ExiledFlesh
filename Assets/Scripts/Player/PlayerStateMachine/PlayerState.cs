using UnityEngine;

public class PlayerState{

    protected PlayerStateMachine stateMachine;
    protected PlayerController controller;

    public PlayerState(PlayerStateMachine stateMachine, PlayerController controller){
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void FrameUpdate(){}
    public virtual void PhysicsUpdate(){}
}