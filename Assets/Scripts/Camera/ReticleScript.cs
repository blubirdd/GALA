using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using TMPro;


public class ReticleScript : MonoBehaviour
{
    [SerializeField] private Image reticle;
    [SerializeField] private LayerMask layerMask;


    [Header("Discovery Status")]
    public GameObject discoveryPanel;

    [SerializeField] private TextMeshProUGUI discoveryStatus;
    [SerializeField] private Image discoveryStatusImage;

    [Header("Discovery Status - Sprites")]
    [SerializeField] private Sprite undiscoveredSprite;
    [SerializeField] private Sprite discoveredCrosshair;
    [SerializeField] private Sprite normalCrosshair;
    private bool _isDiscovered;

    private IAnimal _animal;
    Book book;
    RaycastHit hit;

    private void Start()
    {
        //cache the book instance
        book = Book.instance;
    }
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
            reticle.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            reticle.sprite = discoveredCrosshair;

            //enable panel
            discoveryPanel.SetActive(true);

            //check discovery status

            _animal = hit.collider.GetComponent<IAnimal>();

            for (int i = 0; i < book.photosInventory.Count; i++)
            {
                if (book.photosInventory[i].name == _animal.animalName)
                {
                    discoveryStatusImage.sprite = book.photosInventory[i].polaroidPhoto;
                    discoveryStatus.SetText(_animal.animalName);
                    _isDiscovered = true;
                }

            }

            if(_isDiscovered == false)
            {
                discoveryStatusImage.sprite = undiscoveredSprite;
                discoveryStatus.SetText("Undiscovered");
            }

        }
        else
        {
            reticle.GetComponent<Image>().color = new Color32(255, 231, 217, 255);
            reticle.sprite = normalCrosshair;
            //reset discovery
            _isDiscovered = false;

            //disable panel
            discoveryPanel.SetActive(false);
        }
    }

}
