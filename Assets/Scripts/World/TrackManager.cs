using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    private GameObject[] tracks; 
    private int currentTrackIndex = 0;

    private void Start()
    {
        // fill the tracks array with the children of this GameObject
        tracks = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tracks[i] = transform.GetChild(i).gameObject;
            tracks[i].SetActive(false);
        }

        //tracks[currentTrackIndex].SetActive(true);
    }

    public void ActivateNextTrack()
    {
       
        tracks[currentTrackIndex].SetActive(false);

        // increment the current track index
        currentTrackIndex++;

        // activate the next track
        if (currentTrackIndex < tracks.Length)
        {
            tracks[currentTrackIndex].SetActive(true);
        }

    }
}
