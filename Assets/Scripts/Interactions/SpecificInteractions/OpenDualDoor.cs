using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDualDoor : MonoBehaviour, IEInteractable
{

    [SerializeField] GameObject door1;
    [SerializeField] Transform closePoint1;
    [SerializeField] Transform openPoint1;
    [SerializeField] GameObject door2;
    [SerializeField] Transform closePoint2;
    [SerializeField] Transform openPoint2;
    [SerializeField] float openTime;
    [SerializeField] float interpolateTime;
    public AudioSource openAudio;
    public AudioSource closeAudio;
    bool isOpen = false;


    public virtual void Interact(GameObject interactor){
        openAudio.Play();
        if(isOpen){return;}
        StartCoroutine(MoveDoor(door1,openPoint1,true));
        StartCoroutine(MoveDoor(door2,openPoint2,true));
    }

    public void Interact(){
        if(isOpen){return;}
        StartCoroutine(MoveDoor(door1,openPoint1,true));
        StartCoroutine(MoveDoor(door2,openPoint2,true));  
    }

    IEnumerator MoveDoor(GameObject door,Transform destination, bool activateDelay){
        while(!HasArrived(door.transform.position,destination.position)){
            door.transform.position = Vector3.Lerp(door.transform.position, destination.position,interpolateTime * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        isOpen = activateDelay;
        if(activateDelay){StartCoroutine(Delay());}

    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(openTime);
        closeAudio.Play();
        StartCoroutine(MoveDoor(door1,closePoint1,false));
        StartCoroutine(MoveDoor(door2,closePoint2,false));
    }

    
    bool HasArrived(Vector3 pointA, Vector3 pointB){
        return pointA.x < pointB.x + 0.01 && pointA.x > pointB.x - 0.01
             && pointA.y < pointB.y + 0.01 && pointA.y > pointB.y - 0.01
             && pointA.z < pointB.z + 0.01 && pointA.z > pointB.z - 0.01;
    }
}
