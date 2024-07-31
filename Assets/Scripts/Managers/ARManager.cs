using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager
{
    private ARCameraManager arCameraManager;
    private ARSession arSession;
    private ARInputManager arInputManager;

    public void GetARFunction(ARCameraManager cameraManager, ARSession session, ARInputManager inputManager)
    {
        arCameraManager = cameraManager;
        arSession = session;
        arInputManager = inputManager;
    }

    public void SetARActive(bool value)
    {
        arCameraManager.enabled = value;
        arSession.enabled = value;
        arInputManager.enabled = value;
    }
}
