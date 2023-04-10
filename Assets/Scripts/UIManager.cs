using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of UIManager found");
            return;
        }

        instance = this;
    }
    #endregion

    //this is outdated
    public GameObject objectiveListCanvas;

    [Header("Animations")]
    // [SerializeField] private float fadeTime = 1f;
    [Header("Player")]
    public GameObject playerParent;
    public GameObject playerArmature;
    public GameObject crouchGradientPanel;

    [Header("Controls")]
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject inGameCameraCanvas;
    public GameObject playerInputUI;

    public GameObject playerModel;

    public GameObject pauseMenu;

    public UIVirtualJoystick uiVirtualJoystick;

    [Header("MOVEMENT BUTTON")]
    public GameObject movementButtons;

    [Header("SUBTLE NOTIFICATION")]
    public GameObject subtleNotificationPanel;

    [Header("HAND EQUIPMENTS")]
    public GameObject throwButtonsParent;
    public GameObject throwButton;
    public GameObject unequipButton;
    public GameObject aimButton;
    public GameObject fishcastButton;

    [Header("Camera")]
    public bool cameraOpened;

   //[Header("Inventory")]
   // public GameObject inventoryPanel;


    [Header("Book")]
    public GameObject book;


    [Header("BUTTONS PARENT")]
    public GameObject buttonsUIPack;

    [Header("Tutorial")]
    public GameObject tutorialCanvas;
    //task ui is on taskui script
    // public TaskUI taskUI;

    //[Header("NEW INPUT SYSTEM")]
    //public InputAction playerControl;

    [Header("Quest Notification")]
    [SerializeField] private GameObject paperNotifCanvas;

    [Header("Quest UI")]
    public GameObject questUI;

    [Header("Outdated")]
    //THIS IS OUTDATED
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;

    public GameObject analytics;
    public Goal goal;


    [Header("PLAYER EMOTE")]
    public GameObject emote;
    public GameObject hunterEmote;

    public bool disableFpsLimit = false;
    private void Start()
    {
        //camera 

        inGameCameraCanvas.SetActive(false);

        //events
        // GameEvents.onQuestAccepted += GetObjectives;

        GameEvents.instance.onQuestCompleted += ClearObjectiveList;


    }

    private void Update()
    {

    }


    public void ToggleFPS()
    {

        disableFpsLimit = !disableFpsLimit;

        if (disableFpsLimit == true)
        {
            Application.targetFrameRate = 60;
            Debug.Log("Increase limit to 60");
        }

        else
        {
            Application.targetFrameRate = -1;
            Debug.Log("Decrease limit to 30");
        }


    }

    public void AnalyticsToggle()
    {
        analytics.SetActive(!analytics.activeSelf);
    }
    //public void CloseInventory()
    //{
    //    inventoryPanel.SetActive(false);
    //    Debug.Log("Working close inventory");
    //}

    public void OpenCloseTutorialPrompt()
    {
        tutorialCanvas.SetActive(!tutorialCanvas.activeSelf);
    }

    public void DisableButtonsUIPACK()
    {
       // buttonsUIPack.SetActive(false);
        DisablePlayerMovement();
    }

    public void EnableButtonsUIPACK()
    {
       // buttonsUIPack.SetActive(true);
        EnablePlayerMovement();
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        EnableButtonsUIPACK();

    }



    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        DisableButtonsUIPACK();
    }

    //controls
    public void OpenCamera()
    {
        DisablePlayerMovement();
        SwitchToFirstPerson();

        cameraOpened = true;

        GameEvents.instance.CameraOpened();


        //debug purpose
       // Application.targetFrameRate = 60;
    }
    public void CloseCamera()
    {
        EnablePlayerMovement();
        SwitchToThirdPerson();

        cameraOpened = false;
        GameEvents.instance.CameraClosed();


        //debug purpose
       // Application.targetFrameRate = -1;
    }
    public void DisablePlayerMovement()
    {
        uiVirtualJoystick.ResetJoyStick();
        playerInputUI.SetActive(false);
        questUI.SetActive(false);
        // playerModel.SetActive(false);
    }

    public void EnablePlayerMovement()
    {
        uiVirtualJoystick.ResetJoyStick();
        playerInputUI.SetActive(true);
        questUI.SetActive(true);
        //playerModel.SetActive(true);
    }

    public void SwitchToFirstPerson()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;

        // Rotate the player with camera rotation
        playerArmature.transform.rotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        StartCoroutine(WaitForSeconds());
        
        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(1f);
            inGameCameraCanvas.SetActive(true);

        }

        //thirdPersonCamera.SetActive(false);
        //firstPersonCamera.SetActive(true);
    }
    public void SwitchToThirdPerson()
    {
        inGameCameraCanvas.SetActive(false);

        //thirdPersonCamera.SetActive(true);
        //firstPersonCamera.SetActive(false);

    }

    public void OpenObjectiveList()
    {
        objectiveListCanvas.SetActive(!objectiveListCanvas.activeSelf);
    }

    // void GetObjectives(string title, List<Goal> list)
    // {

    //     for(int i = 0; i < list.Count; i++)
    //     {
    //         if (i == 0)
    //         {
    //             objective1.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
    //         }

    //         if (i == 1)
    //         {
    //             objective2.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
    //         }

    //         if (i == 2)
    //         {
    //             objective3.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
    //         }

    //     }


    // }

    public void ClearObjectiveList(string questName)
    {
        objective1.SetText("No Active Quest/Objective");
        objective2.SetText("");
        objective3.SetText("");

    }

    public void OpenBook()
    {
        book.SetActive(!book.activeSelf);

        DisablePlayerMovement();

        //DisableButtonsUIPACK();
    }

    public void CloseBook()
    {
        EnablePlayerMovement();
        book.SetActive(!book.activeSelf);

       // EnableButtonsUIPACK();
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void SetButtonPanels(bool state)
    {
        buttonsUIPack.SetActive(state);
    }

    public void SetMovementButtons(bool state)
    {
        movementButtons.SetActive(state);
    }


    //THROWING
    public void EnableThrowUI()
    {
        throwButton.SetActive(true);
        EnableUnequipButton();
    }

    public void DisableThrowUI()
    {
        throwButton.SetActive(false);
        DisableAimUI();
        DisableUnequipButton();
    }

    public void EnableAimUI()
    {
        aimButton.SetActive(true);
    }

    public void DisableAimUI()
    {
        aimButton.SetActive(false);
    }

    public void EnableUnequipButton()
    {
        unequipButton.SetActive(true);
    }

    public void DisableUnequipButton()
    {
        unequipButton.SetActive(false);
    }

    public void EnableFishCastButton()
    {
        SetMovementButtons(false);
        buttonsUIPack.SetActive(false);
        fishcastButton.SetActive(true);
    }

    public void DisableFishCastButton()
    {
        SetMovementButtons(true);
        buttonsUIPack.SetActive(true);
        fishcastButton.SetActive(false);
    }


    


}
