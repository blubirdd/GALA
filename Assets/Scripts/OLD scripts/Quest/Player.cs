using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class Player : MonoBehaviour, IDataPersistence
{
    #region Singleton
    public static Player instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of player found");
            return;
        }

        instance = this;

       playerName = PlayerPrefs.GetString("name");
    }
    #endregion
    public static string playerName;
    [Header("Quiz Scores")]
    public int villageQuizScore;
    public int grasslandQuizScore;
    public int riverQuizScore;
    public int swampQuizScore;
    public int rainForestQuizScore;
    public int galaQuizScore;

    [Header("Game Scores")]
    public int eggGameScore;
    public int moleGameScore;


    [Header("UI Canvases")]
    public GameObject playerDiedCanvas;
    public GameObject playerSpottedCanvas;
    public GameObject playerOutOfTimeCanvas;
    public GameObject playerFellOffCanvas;
    [Header("Respawn Locations")]
    public Transform riverCampRespawnPoint;
    public Transform swampVillageRespawnPoint;
    public Transform hunterVillageRespawnPoint;
    public Transform swampLakeRespawnPoint;

    private void Start()
    {
        Hunter.OnHunterHasSpottedPlayer += ShowPlayerCaughtUI;
        Debug.Log("Player name is " + playerName);
    }
    public void EnablePlayerDiedUI()
    {
        playerDiedCanvas.SetActive(true);
    }
    public void ShowPlayerCaughtUI()
    {
        playerSpottedCanvas.SetActive(true);
    }

    public void EnableSwampLakeUI()
    {
        playerFellOffCanvas.SetActive(true);
    }
    public void Respawn(Transform location, GameObject canvasToActivate)
    {
        canvasToActivate.SetActive(false);
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = location.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void RespawnToSwampVillage()
    {
        playerDiedCanvas.SetActive(false);
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = swampVillageRespawnPoint.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void RespawnAfterCaught()
    {
        playerSpottedCanvas.SetActive(false);
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = hunterVillageRespawnPoint.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void RespawnToRiverCamp()
    {
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = riverCampRespawnPoint.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void RespawnToLake()
    {
        playerFellOffCanvas.SetActive(false);
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = swampLakeRespawnPoint.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void LoadData(GameData data)
    {
        //playerName = data.playerName;

        eggGameScore = data.eggGameScore;
        moleGameScore = data.moleGameScore;

        villageQuizScore = data.villageQuizScore;
        grasslandQuizScore = data.grasslandQuizScore;
        riverQuizScore = data.riverQuizScore;
        swampQuizScore = data.swampQuizScore;
        rainForestQuizScore = data.rainForestQuizScore;

    }

    public void SaveData(GameData data)
    {
        //data.playerName = playerName;

        data.eggGameScore = eggGameScore;
        data.moleGameScore = moleGameScore;

        data.villageQuizScore = villageQuizScore;
        data.grasslandQuizScore = grasslandQuizScore;
        data.riverQuizScore = riverQuizScore;
        data.swampQuizScore = swampQuizScore;
        data.rainForestQuizScore = rainForestQuizScore;
    }
}
