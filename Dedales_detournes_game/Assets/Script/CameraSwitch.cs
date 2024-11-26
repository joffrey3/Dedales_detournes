using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera1, VirtualCamera2, VirtualCamera3, VirtualCamera4;
    public GameObject player;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            VirtualCamera1.Priority = 1;
            VirtualCamera2.Priority = 0;
            VirtualCamera3.Priority = 0;
            VirtualCamera4.Priority = 0;

        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            VirtualCamera1.Priority = 0;
            VirtualCamera2.Priority = 1;
            VirtualCamera3.Priority = 0;
            VirtualCamera4.Priority = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            VirtualCamera1.Priority = 0;
            VirtualCamera2.Priority = 0;
            VirtualCamera3.Priority = 1;
            VirtualCamera4.Priority = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            VirtualCamera1.Priority = 0;
            VirtualCamera2.Priority = 0;
            VirtualCamera3.Priority = 0;
            VirtualCamera4.Priority = 1;
        }
    }
}