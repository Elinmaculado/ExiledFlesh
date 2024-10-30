public class EnemyStateMachine{

    public EnemyState CurrentState{get; private set;}

    public void Initializa(EnemyState startState){
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState newState){
        CurrentState.Exit(); 
        CurrentState = newState;
        CurrentState.Enter();
    }
}
