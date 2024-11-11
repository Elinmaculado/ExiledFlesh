using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertInteraction : MonoBehaviour, IEInteractable
{
    [SerializeField] string message;
    public void Interact(GameObject interactor){
        TextAlert.instance.Alert(message);
    }

}
