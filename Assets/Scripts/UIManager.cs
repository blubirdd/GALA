using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;

public class UIManager : MonoBehaviour
{
    public GameObject objectiveListCanvas;

    [Header("Quest Objective/Goal Texts")]
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;


   private  List<Goal> objectiveList = new List<Goal>();

   public Goal goal;

    private void Start()
    {
        GameEvents.onQuestAccepted += GetObjectives;

        GameEvents.instance.onQuestCompleted += ClearObjectiveList;
      
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

}
