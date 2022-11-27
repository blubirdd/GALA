using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.InputSystem.HID;
// using Cinemachine;

public class PhotoCapture : MonoBehaviour
{ 

    //camera ui buttons
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject inGameCameraCanvas;
    public GameObject playerInputUI;
    public GameObject playerModel;
    //
    public GameObject okButton;

    [SerializeField] private GameObject CameraUIPack;

    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    [SerializeField]  private Animator fadingAnimation;


    public ReticleScript reticleScript;

    [SerializeField] private bool cameraOpened;


    void Start()
    {

        inGameCameraCanvas.SetActive(false);
        okButton.SetActive(false);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        reticleScript.GetComponent<ReticleScript>();
       
    }

    void Update()
    {
        if(cameraOpened == true)
        {
            reticleScript.Ray();
        }
    }

    public void OpenCamera()
    {
        DisablePlayerMovement();
        SwitchToFirstPerson();
        cameraOpened = true;

    }

    public void CloseCamera()
    {
       
       EnablePlayerMovement();
       SwitchToThirdPerson();
       cameraOpened=false;
       
    }

    public void DisablePlayerMovement()
    {
        inGameCameraCanvas.SetActive(true);
        playerInputUI.SetActive(false);
        playerModel.SetActive(false);
    }

    public void EnablePlayerMovement()
    {
        inGameCameraCanvas.SetActive(false);
        playerInputUI.SetActive(true);
        playerModel.SetActive(true);

    }

    public void SwitchToFirstPerson()
    {
        thirdPersonCamera.SetActive(false);
        firstPersonCamera.SetActive(true);
    }
    public void SwitchToThirdPerson()
    {
        thirdPersonCamera.SetActive(true);
        firstPersonCamera.SetActive(false);

    }

    public void TakeSnapShot()
    {
        //screenshot
        if(!viewingPhoto)
        {
            StartCoroutine(CapturePhoto());
            reticleScript.Discovered();

        }
        else
        {
            RemovePhoto();
        }
       
    }


    IEnumerator CapturePhoto()
    {
        //set camera ui to false 
        CameraUIPack.SetActive(false);
        

        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        //test

        screenCapture.ReadPixels(regionToRead, 0, 0,false);
        screenCapture.Apply();
        ShowPhoto();
       
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite =  photoSprite;

        photoFrame.SetActive(true);
        
        fadingAnimation.Play("PhotoFade"); 

       okButton.SetActive(true);

    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);

        //cameraui SHOW
        CameraUIPack.SetActive(true);
        okButton.SetActive(false);
    }




}
