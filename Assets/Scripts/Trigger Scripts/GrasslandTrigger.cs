using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasslandTrigger : MonoBehaviour
{
    public GameObject[] grasslandObjects;
    public GameObject[] villageObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerLocationManager.currentLocation == "Village")
            {
                //enable grassland objects
                foreach (var objects in grasslandObjects)
                {
                    objects.SetActive(true);
                }

                //disable village objects
                foreach (var objects in villageObjects)
                {
                    objects.SetActive(false);
                }

                PlayerLocationManager.currentLocation = "Grassland";
                GameEvents.instance.ChangeLocation();


                return;
            }

            if(PlayerLocationManager.currentLocation == "Grassland")
            {
                //disable grassland objects
                foreach (var objects in grasslandObjects)
                {
                    objects.SetActive(false);
                }

                //enable village objects
                foreach (var objects in villageObjects)
                {
                    objects.SetActive(true);
                }

                PlayerLocationManager.currentLocation = "Village";
                GameEvents.instance.ChangeLocation();

                return;
            }

            
        }
    }
}
