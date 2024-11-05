using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class InteractPuzzle : MonoBehaviour, IEInteractable {

    [SerializeField] UnityEvent interactBeforeSolving;
    [SerializeField] UnityEvent interacAfterSolving;
    [SerializeField] MonoBehaviour puzzle;
    [SerializeField] CinemachineVirtualCamera puzzleCamera;
    [SerializeField] CinemachineVirtualCamera roomCamera;

    UnityEvent currentInteraction;
    

    bool isFocused = false;

    private void Start() {
        puzzle.enabled = false;
        currentInteraction = interactBeforeSolving;
        puzzleCamera.Priority = 0;
        roomCamera.Priority = 0;
    }

    private void Update() {
        if(isFocused && Input.GetKeyDown(KeyCode.Q)){
            ExitPuzzle();
        }
    }

    public void ExitPuzzle(){
        puzzle.enabled = false;
        puzzleCamera.Priority = 0;
        roomCamera.Priority = 1;
        isFocused = false;
    }

    public void EnterPuzzle(){
        Debug.Log("Enter puzzle");
        puzzle.enabled = true;
        puzzleCamera.Priority = 1;
        roomCamera.Priority = 0;
        isFocused = true;
    }

    public void Solve(){
        ExitPuzzle();
        currentInteraction = interacAfterSolving;
    }

    public void Interact(GameObject interactor){
        currentInteraction.Invoke();
    }
}
