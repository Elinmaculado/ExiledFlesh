using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor{

    void OnSceneGUI(){
        FieldOfView fov = (FieldOfView)target; 
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward,360, fov.viewRaduis);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2,false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle/2,false);
        
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRaduis);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRaduis);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fov.visibleTargets){
            Handles.DrawLine(fov.transform.position,visibleTarget.position);
        }
    }
}