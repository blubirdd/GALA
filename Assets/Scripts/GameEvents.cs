using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    //Quest Events
    public event Action onQuestCompleted;
    public event Action onGoalCompleted;


    //Dialogue EVENTS

    public event Action onDialogueStarted;
    


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
    public void GoalCompleted()
    {
        onGoalCompleted?.Invoke();
    }

    public void DialogueStart()
    {
        onDialogueStarted?.Invoke();
    }

}
