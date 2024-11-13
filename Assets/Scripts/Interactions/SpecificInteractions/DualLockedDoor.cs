using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DualLockedDoor : MonoBehaviour, IEInteractable
{

    [SerializeField] GameObject door1;
    [SerializeField] Transform closePoint1;
    [SerializeField] Transform openPoint1;
    [SerializeField] GameObject door2;
    [SerializeField] Transform closePoint2;
    [SerializeField] Transform openPoint2;
    [SerializeField] float openTime;
    [SerializeField] float interpolateTime;
    [SerializeField] KeyDoor neededKey;
    [SerializeField] string lockedMessage = "It's locked";
    public AudioSource openAudio;
    public AudioSource closeAudio;
    bool isOpen = false;
    [SerializeField] UnityEvent onOpen;
    [SerializeField] UnityEvent onUnableToOpen;

    [SerializeField] float onunableToOpenDelay = 0;



    public virtual void Interact(GameObject interactor){
        if(isOpen){return;}
        if(interactor.TryGetComponent(out KeyHolder keyHolder)){
            if(keyHolder.ContainsKey(neededKey.GetKeyType())){
                openAudio.Play();
                StartCoroutine(MoveDoor(door1,openPoint1,true));
                StartCoroutine(MoveDoor(door2,openPoint2,true));
                onOpen.Invoke();
            }
            else{
                TextAlert.instance.Alert(lockedMessage,1.5f);
                StartCoroutine(UnableToOpenEventDelay());
            }
        }
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
        Debug.Log("delay");
        yield return new WaitForSeconds(openTime);
        closeAudio.Play();
        StartCoroutine(MoveDoor(door1,closePoint1,false));
        StartCoroutine(MoveDoor(door2,closePoint2,false));
    }

    IEnumerator UnableToOpenEventDelay()
    {
        yield return new WaitForSeconds(onunableToOpenDelay);
        onUnableToOpen.Invoke();
    }

    bool HasArrived(Vector3 pointA, Vector3 pointB){
        return pointA.x < pointB.x + 0.01 && pointA.x > pointB.x - 0.01
             && pointA.y < pointB.y + 0.01 && pointA.y > pointB.y - 0.01
             && pointA.z < pointB.z + 0.01 && pointA.z > pointB.z - 0.01;
    }
}
