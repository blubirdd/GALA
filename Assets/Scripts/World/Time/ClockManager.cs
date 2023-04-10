using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    #region Singleton

    public static ClockManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ClockManager found");
            return;
        }

        instance = this;
    }
    #endregion

    public ClockUI clockui;

    // Start is called before the first frame update
    public void StartClock(int seconds)
    {
        clockui.gameObject.SetActive(true);
        clockui.UpdateClock(seconds);
    }

    public void DisableClock()
    {
        clockui.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
