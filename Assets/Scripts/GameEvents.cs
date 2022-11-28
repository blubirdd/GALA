using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }

    //Quest UI Events
    public event Action onQuestTitleChange;
    public event Action onQuestDescriptionChange;


    //Goal UI Events
    public delegate void ObjectiveUIEventHander(List<Goal> list);

    public static event ObjectiveUIEventHander onQuestAccepted;

    public static void QuestAccepted(List<Goal> list)
    {
        if (onQuestAccepted != null)
        {
            onQuestAccepted(list);
        }

    }

    public event Action onGoalValueChanged;

    //Quest Events



    public event Action onQuestCompleted;
    public event Action onGoalCompleted;

   
   

    //Dialogue EVENTS

    public event Action onDialogueStarted;


    //Quest UI Events
    public void ChangeQuestTile()
    {
        onQuestTitleChange?.Invoke();
    }

    public void ChangeQuestDescription()
    {
        onQuestDescriptionChange?.Invoke();
    }

    public void QuestCompleted()
    {
        onQuestCompleted?.Invoke();
    }

    //Goal UI Events
    public void GoalValueChanged()
    {
        onGoalValueChanged?.Invoke();
    }

    //Quest Events


    public void GoalCompleted()
    {
        onGoalCompleted?.Invoke();
    }

    //Dialogue EVENTS
    public void DialogueStart()
    {
        onDialogueStarted?.Invoke();
    }

}
