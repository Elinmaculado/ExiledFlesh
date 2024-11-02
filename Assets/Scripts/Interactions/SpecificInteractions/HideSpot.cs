using System.Collections;
using UnityEngine;

public class HideSpot : MonoBehaviour, IEInteractable
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform hiddingPoint;

    public void Interact(GameObject interactor){
        if(interactor.TryGetComponent(out PlayerController playerController)){
            playerController.PlayerHiddingState.SetPoints(startPoint.position,hiddingPoint.position);
            playerController.StateMachine.ChangeState(playerController.PlayerHiddingState);
        }
    }

}