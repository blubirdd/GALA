using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampTrigger : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerLocationManager.currentLocation == "River")
            {
                //enable swamp objects
            }

            if (PlayerLocationManager.currentLocation == "Swamp")
            {
                //disable swamp objects, enable river objects
            }
        }
    }
}
