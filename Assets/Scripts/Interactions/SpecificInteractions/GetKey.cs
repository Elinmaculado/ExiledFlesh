using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour, IEInteractable
{
    [SerializeField] Key key;

    public void Interact(GameObject interactor)
    {
        key.AddKey(interactor);
    }
}
