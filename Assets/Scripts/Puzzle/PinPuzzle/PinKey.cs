using System;
using UnityEngine;

public class PinKey : MonoBehaviour{
    [SerializeField] char input;
    [SerializeField] PinPuzzle pinPuzzle;
    [SerializeField] KeyType type; 
    
    [Serializable]
    enum KeyType{
        input, 
        clear,
        check
    }

    private void OnMouseDown() {
        switch(type){
                case KeyType.input:
                    pinPuzzle.InputNumber(input);
                    break;
                case KeyType.clear:
                    pinPuzzle.Clear();
                    break;
                case KeyType.check:
                    pinPuzzle.Check();
                    break;
        }
    }
}