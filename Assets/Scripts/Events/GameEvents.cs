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

    //World Events
    public event Action onCutSceneEnter;
    public void CutscenePlay()
    {
        onCutSceneEnter?.Invoke();
    }



    //Quest UI Events
    public event Action onQuestTitleChange;
    public event Action onQuestDescriptionChange;

    


    //Goal UI Events


    public event Action onGoalValueChanged;



    public event Action onGoalCompleted;

   
   

    //Dialogue EVENTS

    public event Action onDialogueStarted;
    public void DialogueStarted()
    {
        onDialogueStarted?.Invoke();
    }

    //Camera Events
    public event Action onCameraOpened;
    public event Action onCameraClosed;

    public void CameraOpened()
    {
        onCameraOpened?.Invoke();
    }
    public void CameraClosed()
    {
        onCameraClosed?.Invoke();
    }
    //

    //Quest UI Events
    public void ChangeQuestTile()
    {
        onQuestTitleChange?.Invoke();
    }

    public void ChangeQuestDescription()
    {
        onQuestDescriptionChange?.Invoke();
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

    //Quest Events

    public event Action<string> onQuestCompleted;
    public void QuestCompleted(string questName)
    {
        onQuestCompleted?.Invoke(questName);
    }

    //Delegates
    public delegate void ObjectiveUIEventHander(string title, List<Goal> list);

    public static event ObjectiveUIEventHander onQuestAccepted;

    public static void QuestAccepted(string title, List<Goal> list)
    {
        if (onQuestAccepted != null)
        {
            onQuestAccepted(title ,list);
        }

    }
}
