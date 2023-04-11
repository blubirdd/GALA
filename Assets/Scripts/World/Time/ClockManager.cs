using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    public void StartClock(int seconds, QuestNew quest)
    {
        clockui.gameObject.SetActive(true);
        //clockui.duration = seconds;
        clockui.UpdateClock(seconds, quest);

    }

    public void TimerEnd(QuestNew quest)
    {
        //if player ran out oftime
        if (!quest.questCompleted)
        {
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
