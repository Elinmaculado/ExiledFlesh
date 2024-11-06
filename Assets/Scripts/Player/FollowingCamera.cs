using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    public Transform player;
    public CinemachineVirtualCamera currentCamera;
    public bool followX;
    public bool followZ;
    public float smoothing;
    bool isFollowing;
    Vector3 newPosition;

    [SerializeField] Transform minClamp;
    [SerializeField] Transform maxClamp;
    

    private void Start()
    {
        currentCamera.Priority = 0;
        newPosition = currentCamera.transform.position;
    }

    private void Update()
    {
        if (isFollowing)
        {
            newPosition = currentCamera.transform.position;
            if (followX) { 
                newPosition.x = player.position.x; 
                newPosition.x = Mathf.Clamp(newPosition.x,minClamp.transform.position.x,maxClamp.transform.position.x);
            }
            if (followZ) { 
                newPosition.z = player.position.z; 
                newPosition.z = Mathf.Clamp(newPosition.z,minClamp.transform.position.z,maxClamp.transform.position.z);
            }
            currentCamera.transform.position = newPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCamera.Priority = 1;
            isFollowing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCamera.Priority = 0;
            isFollowing = false;
        }
    }
}
