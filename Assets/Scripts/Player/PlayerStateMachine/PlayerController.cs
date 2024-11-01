using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed; // Velocidad de avance y retroceso
    public float rotationSpeed = 100f; // Velocidad de rotación 
    public float walkingSpeed = 5f;
    public float sprintSpeed = 8f;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerHiddenState PlayerHiddenState { get; private set; }


    private void Start(){
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(StateMachine, this);
        PlayerHiddenState = new PlayerHiddenState(StateMachine,this);

        StateMachine.Initializa(PlayerIdleState);
    }

    private void Update() {
        StateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}
