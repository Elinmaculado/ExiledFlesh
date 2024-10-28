using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PinPuzzle : MonoBehaviour
{
    [SerializeField] int maxSize = 4;
    [SerializeField] string pass;
    [SerializeField] TextMeshProUGUI display;
    private string guess = "";


    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            Clear();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            Check();
        }
    }
    public void InputNumber(char input){
        if(guess.Length<maxSize){
            guess += input;
            display.text = guess;
        }
    }
    public void Clear(){
        guess = "";
        display.text = guess;
    }

    public void Check(){
        if(pass == guess){
            Debug.Log("correct");
        }
        else{
            Debug.Log("incorrect");
        }
        Clear();
    }
}