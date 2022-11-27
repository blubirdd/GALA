using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{

    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;

        
    }

    private void LateUpdate()
    {
       var rotation = _mainCam.transform.rotation;
       transform.LookAt(worldPosition: transform.position + rotation * Vector3.forward, rotation * Vector3.up);


    }

}
