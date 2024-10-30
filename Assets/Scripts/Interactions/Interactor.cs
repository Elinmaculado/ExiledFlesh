using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour{

    public Transform interactorStart;
    public float interactionRange;
    public LayerMask collitionLayers;


    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Ray ray = new Ray(interactorStart.position, interactorStart.forward);
            Debug.DrawRay(interactorStart.position, interactorStart.forward * interactionRange, Color.magenta,0.5f);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, interactionRange, collitionLayers)){
                if(hitInfo.collider.gameObject.TryGetComponent(out IEInteractable interactableObject)){
                    interactableObject.Interact(this);
                }
            }
        }
    }
}
