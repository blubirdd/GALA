using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLocationManager : MonoBehaviour, IDataPersistence
{
    #region Singleton

    public static PlayerLocationManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerLocationManager found");
            return;
        }

        instance = this;
    }
    #endregion
    public static string currentLocation;
    public GameObject locationPanel;
    public TextMeshProUGUI locationText;

    public GameObject villageLocation;
    public GameObject grasslandLocation;
    public GameObject riverLocation;
    public GameObject swampLocation;
    public GameObject rainforestLocation;

    public float displayTime = 3f;

    public string displayLocationInInspector;

    void Start()
    {
        GameEvents.instance.onLocationChange += LocationChange;
        LocationChange();

        SetCurrentLocationActive();


    }

    public void SetCurrentLocationActive()
    {
        villageLocation.SetActive(currentLocation == "Village");
        grasslandLocation.SetActive(currentLocation == "Grassland");
        riverLocation.SetActive(currentLocation == "River");
        swampLocation.SetActive(currentLocation == "Swamp");
        rainforestLocation.SetActive(currentLocation == "Rainforest");

        displayLocationInInspector = currentLocation;
    }
    public void LocationChange()
    {
        StopAllCoroutines();
        StartCoroutine(DisplayLocationChangePanel());
        IEnumerator DisplayLocationChangePanel()
        {
            locationText.text = PlayerLocationManager.currentLocation;
            locationPanel.SetActive(true);

            yield return new WaitForSeconds(displayTime);

            locationPanel.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        currentLocation = data.playerLocation;
    }

    public void SaveData(GameData data)
    {
        data.playerLocation = currentLocation;
    }
}
