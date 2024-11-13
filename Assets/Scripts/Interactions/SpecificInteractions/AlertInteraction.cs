using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlertInteraction : MonoBehaviour, IEInteractable
{
    [SerializeField] string message;
    [SerializeField] float onInteractDelay = 0;
    [SerializeField] UnityEvent onInteract;
    public void Interact(GameObject interactor){
        TextAlert.instance.Alert(message);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(onInteractDelay);
        onInteract.Invoke();
    }
}
