using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject objectiveListCanvas;

    [Header("Quest Objective/Goal Texts")]
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;

   // private static List<Goal> objectiveList = new List<Goal>();
    private void Start()
    {
        GameEvents.onQuestAccepted += GetObjectives;
    }
    public void OpenObjectiveList()
    {
        objectiveListCanvas.SetActive(!objectiveListCanvas.activeSelf);
    }

    void GetObjectives(List<Goal> list)
    {
        objective1.SetText(list[0].description+" " + list[0].currentAmount + "/" + list[0].requiredAmount);

        
    }

    public void UpdateObjectiveList()
    {
      //  objective1.SetText(objectiveList[0].description);
    }

}
