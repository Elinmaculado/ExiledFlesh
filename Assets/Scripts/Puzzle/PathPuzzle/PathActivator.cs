using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathActivator : MonoBehaviour{
    [SerializeField] PathPuzzle pathPuzzle;
    [SerializeField] char guessValue;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            pathPuzzle.AddGuess(guessValue);
        }
    }
}
