using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomInOut : MonoBehaviour
{


    public float zoomAmount = 15f;
    CinemachineVirtualCamera mainCamera;

// Start is called before the first frame update
    private void Awake()
    {
        mainCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
       mainCamera.m_Lens.FieldOfView = zoomAmount;
    }

    public void Slider(float zoom)
    {
        zoomAmount = zoom; 
    }
}
