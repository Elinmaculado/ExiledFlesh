using System.Collections;
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

    [SerializeField] Animator animator;
    private string currentAnimation;

    [SerializeField] GameObject playerModel;
    [SerializeField] CapsuleCollider playerCollider;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerHiddenState PlayerHiddenState { get; private set; }
    public PlayerHiddingState PlayerHiddingState {get; private set;}
    public PlayerDamagedState PlayerDamagedState {get; private set;}
    public PlayerPuzzleState PlayerPuzzleState {get; private set;}


    private void Start(){
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(StateMachine, this);
        PlayerHiddenState = new PlayerHiddenState(StateMachine,this);
        PlayerHiddingState = new PlayerHiddingState(StateMachine,this);
        PlayerDamagedState = new PlayerDamagedState(StateMachine, this) ;
        PlayerPuzzleState = new PlayerPuzzleState(StateMachine,this);

        StateMachine.Initializa(PlayerIdleState);

        SetAnimation("Idle");
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

    public void EnterDamage(float stateDuration){
        StartCoroutine(DamageStay(stateDuration));
    }

    IEnumerator DamageStay(float duration){
        StateMachine.ChangeState(PlayerDamagedState);
        yield return new WaitForSeconds(duration);
        StateMachine.ChangeState(PlayerIdleState);
    }

    public void SetAnimation(string animation, float crossFadeTime = 0.2f){
        if(currentAnimation == animation){return;}
        currentAnimation = animation;
        animator.CrossFade(animation,crossFadeTime);
    }

    public void SetPlayerVisibility(bool visibility){
        playerModel.SetActive(visibility);
        playerCollider.enabled = visibility;
        rb.useGravity = visibility;
        SetMeshState(visibility);
    }

    public void SetModel(GameObject newModel){
        playerModel.SetActive(false);
        newModel.SetActive(true);
        playerModel = newModel;
        animator = playerModel.GetComponent<Animator>();
    }
}
