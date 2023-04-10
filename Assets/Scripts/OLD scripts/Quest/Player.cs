using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
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

    [Header("UI Canvases")]
    public GameObject playerDiedCanvas;
    public GameObject playerSpottedCanvas;

    [Header("Respawn Locations")]
    public Transform swampVillageRespawnPoint;
    public Transform hunterVillageRespawnPoint;
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
}
