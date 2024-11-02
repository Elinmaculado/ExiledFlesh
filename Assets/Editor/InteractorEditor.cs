using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(Interactor))]
public class InteractorEdito : Editor{
    
    void OnSceneGUI(){
        Interactor interactor = (Interactor)target;
        Handles.color = Color.magenta;
        Handles.DrawLine(interactor.interactorStart.position, interactor.interactorStart.position + (interactor.interactorStart.forward * interactor.interactionRange));
    }
}
