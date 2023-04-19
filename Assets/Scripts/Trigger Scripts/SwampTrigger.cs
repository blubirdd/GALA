using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampTrigger : MonoBehaviour
{
    public GameObject[] swampObjects;
    public GameObject[] riverObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //when entering the swamp
            if (PlayerLocationManager.currentLocation == "River")
            {
                //enable river objects
                foreach (var objects in swampObjects)
                {
                    objects.SetActive(true);
                }

                //disable river objects
                foreach (var objects in riverObjects)
                {
                    objects.SetActive(false);
                }

                PlayerLocationManager.currentLocation = "Swamp";
                GameEvents.instance.ChangeLocation();

                return;
            }

            //when exiting the swamp back to river

            if (PlayerLocationManager.currentLocation == "Swamp")
            {
                //disable swamp objects
                foreach (var objects in swampObjects)
                {
                    objects.SetActive(false);
                }

                //enable river objects
                foreach (var objects in riverObjects)
                {
                    objects.SetActive(true);
                }

                PlayerLocationManager.currentLocation = "River";
                GameEvents.instance.ChangeLocation();

                return;
            }
        }
    }
}
