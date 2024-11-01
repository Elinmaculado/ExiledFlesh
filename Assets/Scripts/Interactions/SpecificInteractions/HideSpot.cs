using UnityEngine;

public class HideSpot : MonoBehaviour, IEInteractable
{
    public void Interact(GameObject interactor){
        if(interactor.TryGetComponent(out PlayerController playerController)){
            playerController.StateMachine.ChangeState(playerController.PlayerHiddenState);
        }
    }
}