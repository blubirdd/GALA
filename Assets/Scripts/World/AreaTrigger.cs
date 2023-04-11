using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public float fogStartDistance = 5f; // The fog start distance to set
    public float fogEndDistance = 50f; // The fog end distance to set

    public bool changeFogColor = false;
    public Color fogColor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the trigger was triggered by the player object
        {
            // Set the fog start and end distances
            RenderSettings.fog = true;
            RenderSettings.fogStartDistance = fogStartDistance;
            RenderSettings.fogEndDistance = fogEndDistance;

            if (changeFogColor)
            {
                //change to timecontroller function
                RenderSettings.fogColor = fogColor;
            }

        }
    }
}
