using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    #region Singleton

    public static WaypointManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of waypointmanager found");
            return;
        }

        instance = this;
    }
    #endregion
    public List<Transform> waypointTransforms = new List<Transform> ();


}
