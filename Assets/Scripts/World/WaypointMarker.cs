using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    public Transform location;
    public GameObject waypointUI;

    public void SpawnWaypointMarker()
    {
        GameObject go = Instantiate(waypointUI);
        go.GetComponent<WaypointUI>().SetTarget(location);
        go.name = location.name + "Waypoint";
    }



}
