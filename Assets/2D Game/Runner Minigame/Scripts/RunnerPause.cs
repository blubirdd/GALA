using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPause : MonoBehaviour
{
   #region Singleton

    public static RunnerPause instance;

    void Awake()
    {
        PauseGame();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of RunnerPause found");
            return;
        }

        instance = this;
    }
    #endregion


    public GameObject hitPrefab;

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}

