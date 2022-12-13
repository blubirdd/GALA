using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.InputSystem.HID;
using TMPro;
// using Cinemachine;

public class PhotoCapture : MonoBehaviour
{ 

    public GameObject okButton;

    [SerializeField] private GameObject CameraUIPack;

    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    [SerializeField]  private Animator fadingAnimation;


    public ReticleScript reticleScript;
    public UIManager uiManager;

   
    [SerializeField] private TextMeshProUGUI animalText;


    void Start()
    {

        okButton.SetActive(false);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        reticleScript.GetComponent<ReticleScript>();

        //clear text
        animalText.SetText("");

        //subscribe to picturetaken event
        PictureEvents.onAnimalDiscovered += SetText;

    }

    void Update()
    {
        if(uiManager.cameraOpened == true)
        {
            reticleScript.Ray();
        }
    }

    void SetText(IAnimal animal)
    {   
        animalText.SetText(animal.animalName +" ("+ animal.animalGroup + ")");
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

        //clear text
        animalText.SetText("");
    }




}
