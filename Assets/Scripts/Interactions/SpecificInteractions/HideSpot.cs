using UnityEngine;

public class HideSpot : MonoBehaviour, IEInteractable
{
    public void Interact(Interactor interactor){
        if(interactor.gameObject.CompareTag("Player")){
            interactor.tag = "HidenPlayer";
        }
        else{
            interactor.tag = "Player";
        }
    }
}