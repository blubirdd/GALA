using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Discovery : MonoBehaviour
{
    public TextMeshProUGUI locationName;

    public void SetName(string text)
    {
        locationName.SetText(text);
    }

    public void DestroyUI()
    {

        //ui
        GameEvents.instance.CameraClosed();
        UIManager.instance.EnablePlayerMovement();

        GameObject.Destroy(this.gameObject);
    }

}
