using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFix : MonoBehaviour
{
    public GameObject grass;
    public TimeController timeController;
    private void OnEnable()
    {
        if(timeController.timeHour >= 19 || timeController.timeHour < 6)
        grass.SetActive(false);
    }

    private void OnDisable()
    {
        grass.SetActive(true);
    }
}
