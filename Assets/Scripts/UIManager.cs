using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

     [Header("Controls")]
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject inGameCameraCanvas;
    public GameObject playerInputUI;

    public GameObject playerModel;

    public GameObject pauseMenu;

    [Header("Camera")]
    public bool cameraOpened;


    [Header("Book")]
    public GameObject book;


    [Header("BUTTONS PARENT")]
    public GameObject buttonsUIPack;

    [Header("Quest Objective/Goal Texts")]
    //task ui is on taskui script
    // public TaskUI taskUI;

    //THIS IS OUTDATED
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;


    public Goal goal;

    private void Start()
    {
        //camera 

        inGameCameraCanvas.SetActive(false);

        //events
        GameEvents.onQuestAccepted += GetObjectives;

        GameEvents.instance.onQuestCompleted += ClearObjectiveList;

    }

    public void DisableButtonsUIPACK()
    {
        buttonsUIPack.SetActive(false);
    }

    public void EnableButtonsUIPACK()
    {
        buttonsUIPack.SetActive(true);
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

    }
    public void CloseCamera()
    {
        EnablePlayerMovement();
        SwitchToThirdPerson();

        cameraOpened = false;
        GameEvents.instance.CameraClosed();
    }
    public void DisablePlayerMovement()
    {

        playerInputUI.SetActive(false);
        playerModel.SetActive(false);
    }

    public void EnablePlayerMovement()
    {
        
        playerInputUI.SetActive(true);
        playerModel.SetActive(true);

    }

    public void SwitchToFirstPerson()
    {
        inGameCameraCanvas.SetActive(true);

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

    void GetObjectives(string title, List<Goal> list)
    {

        for(int i = 0; i < list.Count; i++)
        {
            if (i == 0)
            {
                objective1.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
            }

            if (i == 1)
            {
                objective2.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
            }

            if (i == 2)
            {
                objective3.SetText(list[i].description + ": " + "<color=#FAF84A> " + list[i].currentAmount + "/" + list[i].requiredAmount + "</color>");
            }

        }


    }

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

        DisableButtonsUIPACK();
    }

    public void CloseBook()
    {
        EnablePlayerMovement();
        book.SetActive(!book.activeSelf);

        EnableButtonsUIPACK();
    }

    public void ChangeSceneToForest()
    {
        SceneManager.LoadScene("Forest");
    }

}
