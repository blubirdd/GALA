using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;


public class ReticleScript : MonoBehaviour
{
    [SerializeField] private Image reticle;
    [SerializeField] private LayerMask layerMask;

    RaycastHit hit;

    public void Discovered()
    {
        if (hit.collider != null)
        {
            hit.collider.gameObject.SendMessage("Discovered");
        }
    }
    public void Ray()
    {
        Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);

       
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50f, layerMask))// && hit.transform.gameObject.CompareTag("NPC"))
        {
            reticle.GetComponent<Image>().color = new Color32(255, 255, 60, 100);
        }
        else
        {
            reticle.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }

}
