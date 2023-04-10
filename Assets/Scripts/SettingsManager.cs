using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
//public class SettingsManager : MonoBehaviour, IDataPersistence
{

    [Header("Environment")]
    public GameObject grass;
    public GameObject graphy;
    private bool fpsLimitEnabled = false;

    [Header("Game Levels")]
    private int currentLevel;

    void Start()
    {
        currentLevel = 0;
    }

    public void ToggleGrass()
    {
        grass.SetActive(!grass.activeSelf);
    }

    public void ToggleGraphy()
    {
        graphy.SetActive(!graphy.activeSelf);

    }

    public void ToggleFpsLimit()
    {
        fpsLimitEnabled = !fpsLimitEnabled;
        if (fpsLimitEnabled)
        {
            Application.targetFrameRate = 30;
        }
        else
        {
            Application.targetFrameRate = 60;
        }
    }

    public void ChangeCurrentLevel(int level)
    {
        currentLevel = level;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //public void LoadData(GameData data)
    //{
    //    currentLevel = data.gameLevel;
    //    Debug.Log("Loading game level: " + data.gameLevel);
    //    Time.timeScale = 1f;
    //}

    //public void SaveData(GameData data)
    //{
    //    data.gameLevel = currentLevel;
    //    Debug.Log("Saving game level data");
    //}
}