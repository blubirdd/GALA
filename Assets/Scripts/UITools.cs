using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class UITools : MonoBehaviour
{
    // Start is called before the first frame update

   

 public GameObject firstPersonCamera;
 public GameObject thirdPersonCamera;
 public GameObject inGameCameraCanvas;

 public GameObject playerInputUI;

    void Start()
    {
      inGameCameraCanvas.SetActive(false);
              
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OpenCamera()
    {
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
        inGameCameraCanvas.SetActive(true);
        playerInputUI.SetActive(false);


      Debug.Log("Camera Opened");
    }

    public void CloseCamera()
    {
       thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);
        inGameCameraCanvas.SetActive(false);
        playerInputUI.SetActive(true);
    }

    
}
