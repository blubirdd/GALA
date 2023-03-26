using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    [Header("Environment")]
    public GameObject grass;
    public GameObject graphy;
    private bool fpsLimitEnabled = false;

    public void ToggleGrass()
    {
        grass.SetActive(!grass.activeSelf);
    }

    public void ToggleGraphy()
    {
        graphy.SetActive(!graphy.activeSelf);

    }

    public void ToggleFpsLimit()
    {
        fpsLimitEnabled = !fpsLimitEnabled;
        if (fpsLimitEnabled)
        {
            Application.targetFrameRate = 30;
        }
        else
        {
            Application.targetFrameRate = -1;
        }
    }



    void Start()
    {
        
    }

}
