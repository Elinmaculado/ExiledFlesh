public class EnemyState {
    
    protected EnemyController controller;
    protected EnemyStateMachine stateMachine;

    public EnemyState(EnemyStateMachine stateMachine, EnemyController controller){
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void FrameUpdate(){}
    public virtual void PhysicsUpdate(){}
}
