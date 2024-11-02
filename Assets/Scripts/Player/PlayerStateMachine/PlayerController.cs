using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float currentSpeed; // Velocidad de avance y retroceso
    public float rotationSpeed = 100f; // Velocidad de rotación 
    public float walkingSpeed = 5f;
    public float sprintSpeed = 8f;
    public Rigidbody rb;

    [SerializeField] List<GameObject> fovMeshes;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerHiddenState PlayerHiddenState { get; private set; }
    public PlayerHiddingState PlayerHiddingState {get; private set;}

    private void Start(){
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(StateMachine, this);
        PlayerHiddenState = new PlayerHiddenState(StateMachine,this);
        PlayerHiddingState = new PlayerHiddingState(StateMachine,this);

        StateMachine.Initializa(PlayerIdleState);
    }

    private void Update() {
        StateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetMeshState(bool state){
        for(int i = 0; i<fovMeshes.Count; i++){
            fovMeshes[i].SetActive(state);
        }
    }
}
