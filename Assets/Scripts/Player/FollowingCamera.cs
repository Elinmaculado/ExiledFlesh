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
            if (followX) { newPosition.x = player.position.x; }
            if (followZ) { newPosition.z = player.position.z; }
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
