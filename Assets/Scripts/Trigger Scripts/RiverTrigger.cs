using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverTrigger : MonoBehaviour
{
    public GameObject[] riverObjects;
    public GameObject[] grasslandObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //when entering the river
            if (PlayerLocationManager.currentLocation == "Grassland")
            {
                //enable river objects
                foreach (var objects in riverObjects)
                {
                    objects.SetActive(true);
                }

                //disable grassland objects
                foreach (var objects in grasslandObjects)
                {
                    objects.SetActive(false);
                }

                PlayerLocationManager.currentLocation = "River";
                GameEvents.instance.ChangeLocation();

                return;
            }

            //when exiting the river back to grassland

            if (PlayerLocationManager.currentLocation == "River")
            {
                //disable river objects
                foreach (var objects in riverObjects)
                {
                    objects.SetActive(false);
                }

                //enable grassland objects
                foreach (var objects in grasslandObjects)
                {
                    objects.SetActive(true);
                }

                PlayerLocationManager.currentLocation = "Grassland";
                GameEvents.instance.ChangeLocation();

                return;
            }


        }
    }
}
