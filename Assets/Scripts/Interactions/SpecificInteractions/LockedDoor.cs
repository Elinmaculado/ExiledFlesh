using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockedDoor : MonoBehaviour, IEInteractable
{

    [SerializeField] GameObject door;
    [SerializeField] Transform openPoint;
    [SerializeField] Transform closePoint;
    [SerializeField] float openTime;
    [SerializeField] float interpolateTime;
    bool isOpen = false;
    [SerializeField] KeyDoor neededKey;
    [SerializeField] string lockedMessage = "It's locked";
    public AudioSource openAudio;
    public AudioSource closeAudio;
    [SerializeField] UnityEvent onOpen;
    [SerializeField] UnityEvent onUnableToOpen;
    [SerializeField] float onunableToOpenDelay = 0;



    public virtual void Interact(GameObject interactor){
        
        if(isOpen){return;}
        if(interactor.TryGetComponent(out KeyHolder keyHolder)){
            if(keyHolder.ContainsKey(neededKey.GetKeyType())){
                openAudio.Play();
                onOpen.Invoke();
                StartCoroutine(MoveDoor(openPoint,true));
            }
            else{
                TextAlert.instance.Alert(lockedMessage,1.5f);
                StartCoroutine(UnableToOpenEventDelay());
            }
        }
    }

    IEnumerator MoveDoor(Transform destination, bool activateDelay){
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
        StartCoroutine(MoveDoor(closePoint,false));
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
