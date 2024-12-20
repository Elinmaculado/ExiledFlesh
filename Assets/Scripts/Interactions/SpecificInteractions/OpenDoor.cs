using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour, IEInteractable
{
    [SerializeField] GameObject door;
    [SerializeField] Transform closePoint;
    [SerializeField] Transform openPoint;
    [SerializeField] float openTime;
    [SerializeField] float interpolateTime;
    bool isOpen = false;
    [SerializeField] UnityEvent onOpen;
    public AudioSource openAudio;
    public AudioSource closeAudio;


    public virtual void Interact(GameObject interactor){
        
        if(isOpen){return;}
        openAudio.Play();
        onOpen.Invoke();
        StartCoroutine(MoveDoor(openPoint,true));
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

    
    bool HasArrived(Vector3 pointA, Vector3 pointB){
        return pointA.x < pointB.x + 0.01 && pointA.x > pointB.x - 0.01
             && pointA.y < pointB.y + 0.01 && pointA.y > pointB.y - 0.01
             && pointA.z < pointB.z + 0.01 && pointA.z > pointB.z - 0.01;
    }
}
