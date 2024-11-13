using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class RotatingPuzzle : MonoBehaviour
{
    Quaternion originalPos;
    [SerializeField] float angleOfsset;
    [SerializeField] GameObject rotationObject;
    bool isShuffled = false;
    [SerializeField] int totalShuffle;
    [SerializeField] UnityEvent onCompletion;
    
    void Awake(){
        originalPos = rotationObject.transform.rotation;
        Shuffle();
    }

    public void Up(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.right);
        CheckCorrect();
    }
    
    public void Down(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.left);
        CheckCorrect();
    }
    
    public void Left(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.back);
        CheckCorrect();
    }
    
    public void Right(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.forward);
        CheckCorrect();
    }
    
    public void Front(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.up);
        CheckCorrect();
    }
    
    public void Back(){
        rotationObject.transform.Rotate(angleOfsset*Vector3.down);
        CheckCorrect();
    }

    void CheckCorrect(){
        if(originalPos == rotationObject.transform.rotation && isShuffled){
            onCompletion.Invoke();
        }
    }

    void Shuffle(){
        isShuffled = false;
        for(int i = 0; i<totalShuffle; i++){
            int randomIndex = Random.Range(0,5);
            switch(randomIndex){
                case 0:
                    Up();
                    break;
                case 1:
                    Down();
                    break;
                case 2:
                    Left();
                    break;
                case 3:
                    Right();
                    break;
                case 4:
                    Front();
                    break;
                case 5:
                    Back();
                    break;
                default:
                    Up();
                    break;
            }
        }
        if(originalPos == rotationObject.transform.rotation){
            Shuffle();
        }
        else{
            isShuffled = true;
        }
    }

  
}
