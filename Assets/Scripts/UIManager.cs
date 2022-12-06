using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject objectiveListCanvas;

     [Header("Controls")]
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public GameObject inGameCameraCanvas;
    public GameObject playerInputUI;

    public GameObject playerModel;

    [Header("Camera")]
    public bool cameraOpened;


    [Header("Book")]
    public GameObject book;

    [Header("Quest Objective/Goal Texts")]
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

    //controls
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
        cameraOpened = false;
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

    public void OpenObjectiveList()
    {
        objectiveListCanvas.SetActive(!objectiveListCanvas.activeSelf);
    }

    void GetObjectives(List<Goal> list)
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

    public void ClearObjectiveList()
    {
        objective1.SetText("No Active Quest/Objective");
        objective2.SetText("");
        objective3.SetText("");

    }

    public void OpenBook()
    {
        book.SetActive(!book.activeSelf);

    }

}
