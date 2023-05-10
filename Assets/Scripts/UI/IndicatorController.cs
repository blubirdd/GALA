using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    #region Singleton

    public static IndicatorController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of IndicatorController found");
            return;
        }

        instance = this;
    }
    #endregion

    [Header("UI Buttons")]
    public GameObject cameraIndicator;
    public GameObject bookIndicator;
    public GameObject toDoIndicator;
    public GameObject bagIndicator;



    [Header("Controls Buttons")]
    public GameObject sneakButton;
    // Start is called before the first frame update

    [Header("BOOK RED CIRCLE")]
    public GameObject bookRedCircle;

    [Header("book caterogies")]
    public GameObject[] categoriesCircle;
    public void EnableBookRedCircle()
    {
        bookRedCircle.SetActive(true);
    }

    public void DisableBookRedCircle()
    {
        bookRedCircle.SetActive(false);
    }
    public void EnableCameraIndicator()
    {
        cameraIndicator.SetActive(true);
    }

    public void DisableCameraIndicator()
    {
        cameraIndicator.SetActive(false);
    }

    //
    public void EnableBookIndicator()
    {
        bookIndicator.SetActive(true);
    }

    public void DisableBookIndicator()
    {
        bookIndicator.SetActive(false);
    }

    //
    public void EnableToDoIndicator()
    {
        toDoIndicator.SetActive(true);
    }
    public void DisableToDoIndicator()
    {
        toDoIndicator.SetActive(false);
    }
    //
    public void EnableBagIndicator()
    {
       bagIndicator.SetActive(true);
    }
    public void DisableBagIndicator()
    {
       bagIndicator.SetActive(false);
    }





}
