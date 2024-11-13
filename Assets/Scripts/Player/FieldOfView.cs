using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class FieldOfView : MonoBehaviour{
    public float viewRaduis;
    [Range(0,360)]
    public float viewAngle;
    
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets;

    public float meshResolution;
    public int edgeResultIteration;
    public float edgeDistanceThreshold;

    public float maskCutAwayDistance = 0.1f;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    
    void Start(){
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine(FindTargetWithDelay(0.2f));
    }

    private void LateUpdate() {
        DrawViewMesh();    
    }

    IEnumerator FindTargetWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets(){
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position,viewRaduis,targetMask);
        for(int i = 0; i < targetInViewRadius.Length; i++){
            Transform target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget)<(viewAngle/2)){
                float distToTarget = Vector3.Distance(transform.position,target.position);
                if(!Physics.Raycast(transform.position,dirToTarget,distToTarget,obstacleMask)){
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal){
        if(!angleIsGlobal){
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));
    }

    void DrawViewMesh(){
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle/stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for(int i = 0; i<=stepCount; i++){
            float angle = transform.eulerAngles.y - (viewAngle/2) + (stepAngleSize * i);
            ViewCastInfo newViewCast = ViewCast(angle);
            
            if(i>0){
                bool isEdgeDistanceThreshholdExceded = Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && isEdgeDistanceThreshholdExceded)){
                    EdgeInfo edge = FindEdge(oldViewCast,newViewCast);
                    if(edge.pointA != Vector3.zero){
                        viewPoints.Add(edge.pointA);
                    }
                      if(edge.pointB != Vector3.zero){
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.hitPoint);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[]  vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2)*3];

        vertices[0] = Vector3.zero;
        for(int i =0; i<vertexCount - 1; i++){
            vertices[i+1]  = transform.InverseTransformPoint(viewPoints[i] + Vector3.forward* maskCutAwayDistance);

            if(i<vertexCount-2){
                triangles[i*3] = 0;
                triangles[(i*3) + 1] = i+1;
                triangles[(i*3) + 2] = i+2;
            }
            
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    public struct ViewCastInfo{
        public bool hit;
        public Vector3 hitPoint;
        public float distance;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _hitPoint, float _distance, float _agle){
            hit = _hit;
            hitPoint = _hitPoint;
            distance = _distance;
            angle = _agle;
        }
    }

    ViewCastInfo ViewCast(float globalAngle){
        Vector3 direction = DirFromAngle(globalAngle,true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, direction,out hit, viewRaduis, obstacleMask)){
            return new ViewCastInfo(true,hit.point,hit.distance,globalAngle);
        }
        else{
            return new ViewCastInfo(false,transform.position + direction * viewRaduis,viewRaduis,globalAngle);
        }
    }

    public struct EdgeInfo{
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB){
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minView, ViewCastInfo maxView){
        float minAngle = minView.angle;
        float maxAngle = maxView.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i<edgeResultIteration; i++){
            float angle = (minAngle + maxAngle)/2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool isEdgeDistanceThreshholdExceded = Mathf.Abs(minView.distance - newViewCast.distance) > edgeDistanceThreshold;
            if(newViewCast.hit == minView.hit && !isEdgeDistanceThreshholdExceded){
                minAngle = angle;
                minPoint = newViewCast.hitPoint;
            }
            else{
                maxAngle = angle;
                maxPoint = newViewCast.hitPoint;
            }
        }

        return new EdgeInfo(minPoint,maxPoint);
    }
}
