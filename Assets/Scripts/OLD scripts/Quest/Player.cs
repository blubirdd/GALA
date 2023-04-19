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
    }
    #endregion
    public static string playerName;
    [Header("Game Scores")]
    public float eggGameScore;
    public float moleGameScore;


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
        playerName = data.playerName;
    }

    public void SaveData(GameData data)
    {
        data.playerName = playerName;
    }
}
