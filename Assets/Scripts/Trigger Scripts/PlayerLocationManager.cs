using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLocationManager : MonoBehaviour
{
    public static string currentLocation;
    public GameObject locationPanel;
    public TextMeshProUGUI locationText;


    public float displayTime = 3f;

    void Start()
    {
        currentLocation = "Village";
        GameEvents.instance.onLocationChange += LocationChange;
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


}
