using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainforestTrigger : MonoBehaviour
{
    public GameObject[] rainforestObjects;
    public GameObject[] swampObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //when entering the rainforest
            if (PlayerLocationManager.currentLocation == "Swamp")
            {
                //enable rainforest objects
                foreach (var objects in rainforestObjects)
                {
                    objects.SetActive(true);
                }

                //disable swamp objects
                foreach (var objects in swampObjects)
                {
                    objects.SetActive(false);
                }

                PlayerLocationManager.currentLocation = "Rainforest";
                GameEvents.instance.ChangeLocation();

                return;
            }

            //when exiting the rainforest back to swamp

            if (PlayerLocationManager.currentLocation == "Rainforest")
            {
                //disable rainforest objects
                foreach (var objects in rainforestObjects)
                {
                    objects.SetActive(false);
                }

                //enable swamp objects
                foreach (var objects in swampObjects)
                {
                    objects.SetActive(true);
                }


                PlayerLocationManager.currentLocation = "Swamp";
                GameEvents.instance.ChangeLocation();

                return;
            }
        }
    }
}
