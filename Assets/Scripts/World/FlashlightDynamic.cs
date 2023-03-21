using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDynamic : MonoBehaviour
{
    public GameObject cameraLight;
    public GameObject grass;
    TimeController timeController;

    //    private void Start()
    //    {
    //        timeController = TimeController.instance;
    //    }
    //    private void OnEnable()
    //    {
    //        if (timeController.timeHour >= 19 || timeController.timeHour < 6)
    //            cameraLight.SetActive(true);
    //    }

    //    private void OnDisable()
    //    {
    //        cameraLight.SetActive(false);
    //    }

    private void Start()
    {
        timeController = TimeController.instance;

        GameEvents.instance.onCameraOpened += FlashLight;
        GameEvents.instance.onCameraClosed += DisableFlashLight;

    }

    public void FlashLight()
    {
        if (timeController.timeHour >= 19 || timeController.timeHour < 6)
        {
            cameraLight.gameObject.SetActive(true);
            grass.SetActive(false);
        }

    }

    public void DisableFlashLight()
    {
        cameraLight.gameObject.SetActive(false);
        grass.SetActive(true);
    }
}
