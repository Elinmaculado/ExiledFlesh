using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FixedCamera : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    private void Start()
    {
        currentCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCamera.Priority = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCamera.Priority = 0;
        }
    }
}
