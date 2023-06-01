using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode mode = Mode.Forward;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                //look from this to camera
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                //find driction from camera to this
                Vector3 dir = transform.position - Camera.main.transform.position;
                //look from this to inversedir camera
                transform.LookAt(transform.position + dir);
                break;
            case Mode.ForwardInverted:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.Forward:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        Forward,
        ForwardInverted
    }
}
