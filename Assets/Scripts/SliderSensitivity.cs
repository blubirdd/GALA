using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSensitivity : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        AdjustSensitivity(3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AdjustSensitivity(float sensitivity)
    {
        UIVirtualTouchZone.sensitivity = sensitivity;
    }
}
