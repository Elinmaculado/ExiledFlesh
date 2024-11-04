using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class InteractPuzzle : MonoBehaviour, IEInteractable{

    [SerializeField] MonoBehaviour puzzle;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineVirtualCamera roomCamera;


    bool isFocused = false;

    private void Start() {
        puzzle.enabled = false;
        virtualCamera.Priority = 0;
    }

    private void Update() {
        if(isFocused && Input.GetKeyDown(KeyCode.E)){
            ExitPuzzle();
        }
    }

    public void ExitPuzzle()
    {
        virtualCamera.Priority = 0;
        roomCamera.Priority = 1;
        isFocused = false;
        puzzle.enabled = false;
    }

    public void Interact(GameObject interactor){
        virtualCamera.Priority = 1;
        roomCamera.Priority = 0;
        isFocused = true;
        puzzle.enabled = true;
    }
}
