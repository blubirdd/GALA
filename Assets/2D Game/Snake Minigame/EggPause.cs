using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPause : MonoBehaviour
{
    #region Singleton

    public static EggPause instance;

    void Awake()
    {
        PauseGame();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EggPause found");
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
