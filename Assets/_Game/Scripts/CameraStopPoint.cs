using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopPoint : MonoBehaviour
{
    public bool AreaCleared { get; private set; }

    private bool m_ActivePoint = false;
    private CameraPathMovement m_CameraPathMovement;

    public void Initialise(CameraPathMovement cameraPathMovement)
    {
        m_CameraPathMovement = cameraPathMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraReachedPoint()
    {
        m_ActivePoint = true;
        m_CameraPathMovement.SetCameraMovement(false);
    }
}
