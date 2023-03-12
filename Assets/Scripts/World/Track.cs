using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    private TrackManager trackManager;

    private void Start()
    {
        trackManager = GetComponentInParent<TrackManager>();
    }

    private void OnDisable()
    {
        if(trackManager != null)
        trackManager.ActivateNextTrack();
    }
}
