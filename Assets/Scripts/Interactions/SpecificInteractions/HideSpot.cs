using System.Collections;
using UnityEngine;

public class HideSpot : MonoBehaviour, IEInteractable
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform hiddingPoint;

    [SerializeField] GameObject door;
    [SerializeField] Transform doorClosedPoint;
    [SerializeField] Transform doorOpenPoint;
    [SerializeField] float interpolateTime;

    public void Interact(GameObject interactor){
        Debug.Log("Hide");
        if(interactor.TryGetComponent(out PlayerController playerController)){
            StopAllCoroutines();
            StartCoroutine(MoveDoor());
            playerController.PlayerHiddingState.SetPoints(startPoint.position,hiddingPoint.position);
            playerController.PlayerHiddenState.hideSpot = this;
            playerController.StateMachine.ChangeState(playerController.PlayerHiddingState);
        }
    }

    bool HasArrived(Vector3 pointA, Vector3 pointB){
        return pointA.x < pointB.x + 0.1 && pointA.x > pointB.x - 0.1
             && pointA.y < pointB.y + 0.1 && pointA.y > pointB.y - 0.1
             && pointA.z < pointB.z + 0.1 && pointA.z > pointB.z - 0.1;
    }


    public IEnumerator MoveDoor(){
        door.transform.position = doorClosedPoint.position;
        while(!HasArrived(door.transform.position,doorOpenPoint.position)){
            door.transform.position = Vector3.Lerp(door.transform.position, doorOpenPoint.position,interpolateTime * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
            while(!HasArrived(door.transform.position,doorClosedPoint.position)){
            door.transform.position = Vector3.Lerp(door.transform.position, doorClosedPoint.position,interpolateTime * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}