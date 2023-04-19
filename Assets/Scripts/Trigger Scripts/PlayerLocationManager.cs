using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLocationManager : MonoBehaviour, IDataPersistence
{
    public static string currentLocation;
    public GameObject locationPanel;
    public TextMeshProUGUI locationText;

    public GameObject villageLocation;
    public GameObject grasslandLocation;
    public GameObject riverLocation;
    public GameObject swampLocation;
    public GameObject rainforestLocation;

    public float displayTime = 3f;

    void Start()
    {
        GameEvents.instance.onLocationChange += LocationChange;
        LocationChange();

        villageLocation.SetActive(currentLocation == "Village");
        grasslandLocation.SetActive(currentLocation == "Grassland");
        riverLocation.SetActive(currentLocation == "River");
        swampLocation.SetActive(currentLocation == "Swamp");
        rainforestLocation.SetActive(currentLocation == "Rainforest");
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
