using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PathPuzzle : MonoBehaviour{

    [SerializeField] string pass;
    string currentGuess;
    int currentIndex = 0;
    [SerializeField] List<GameObject> activators;
    [SerializeField] UnityEvent onSolve;

    public void AddGuess(char guess){
        if(guess == pass[currentIndex]){
            TextAlert.instance.Alert(Message());
            currentGuess += guess;
            currentIndex++;
            if(currentGuess == pass){
                onSolve.Invoke();
                SetActivatorsState(false);
            }
        }
        else{
            TextAlert.instance.Alert("You have lost your path");
            currentGuess = string.Empty;
            currentIndex = 0;
        }
    }

    public void SetActivatorsState(bool state){
        for(int i =0; i<activators.Count; i++){
            activators[i].SetActive(state);
        }
    }

    string Message(){
        string message = string.Empty;
        message = currentIndex switch
        {
            0 => "May the path begin",
            1 => "Always go forth",
            2 => "With patience you will achieve it",
            3 => "Faith will take you far",
            4 => "There may be turns, but you'll make it",
            5 => "Now show yout faith at the church",
            _ => "something went wrong",
        };
        return message;
    }
}
