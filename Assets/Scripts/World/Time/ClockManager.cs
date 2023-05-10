using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour
{
    #region Singleton

    public static ClockManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ClockManager found");
            return;
        }

        instance = this;
    }
    #endregion

    public ClockUI clockui;
    public EggGameManager egggamemanager;
    // Start is called before the first frame update
    public void StartClock(int seconds, QuestNew quest)
    {
        if (clockui.gameObject.activeSelf)
        {
            // If the clock is already running, reset it instead of starting a new one
            clockui.ResetClock();
        }

        else
        {
            clockui.gameObject.SetActive(true);
            //clockui.duration = seconds;
            clockui.UpdateClock(seconds, quest);
        }


    }

    public void TimerEnd(QuestNew quest)
    {
        //if player ran out oftime
        if (!quest.questCompleted)
        {
            //Debug.Log(quest.questID + "IS THIS WORKING");
            quest.RemoveQuest(quest.questID);
            Task.instance.RemoveTaskNotComplete(quest.questID);

            //Player.instance.
            UIManager.instance.PauseGame();
            Player.instance.playerOutOfTimeCanvas.SetActive(true);
            quest.AcceptQuest(quest.questID);
        }

        DisableClock();
        //disable Quest

    }

    public void StartClockIndependent(int seconds)
    {
        clockui.gameObject.SetActive(true);
        //clockui.duration = seconds;
        clockui.UpdateClockIndependent(seconds);
    }

    public void IndependentClockEnd()
    {
        Debug.Log("End game");
        if(egggamemanager != null)
        {
            egggamemanager.BackToGame();
        }
    }

    public void DisableClock()
    {
        clockui.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
