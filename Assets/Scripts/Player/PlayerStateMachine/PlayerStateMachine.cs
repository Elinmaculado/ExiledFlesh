public class PlayerStateMachine{

    public PlayerState CurrentState{get; private set;}

    public void Initializa(PlayerState startState){
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState){
        CurrentState.Exit(); 
        CurrentState = newState;
        CurrentState.Enter();
    }
}
