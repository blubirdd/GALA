using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : MonoBehaviour, IThreat
{
    public string threatName { get; set; }
    [SerializeField] ThreatScriptable threatScriptable;
    [SerializeField] private string threatID;

    void Start()
    {
        threatName = threatScriptable.threatName;
    }

    public void Discovered()
    {
        PictureEvents.ThreatDiscovered(this);

        if (!Book.instance.photosThreat.Contains(threatScriptable))
        {
            Debug.Log("NEWLY ISCOVERED ADDED TO DATABASE: " + threatName);

            StartCoroutine(WaitForPhoto());
            IEnumerator WaitForPhoto()
            {
                yield return new WaitForEndOfFrame();
                Inventory.instance.itemDiscovery.NewItemDiscovered(threatScriptable.threatPicture, threatScriptable.threatName, "New Threat Discovered. Check your journal for more details", false);
            }

        }

        Book.instance.AddThreatPhoto(threatScriptable);
    }

}
