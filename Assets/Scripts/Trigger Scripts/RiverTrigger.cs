using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverTrigger : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerLocationManager.currentLocation == "Grassland")
            {
                //enable river objects
            }

            if (PlayerLocationManager.currentLocation == "River")
            {
                //disable river objects, enable grassland objects
            }
        }
    }
}
