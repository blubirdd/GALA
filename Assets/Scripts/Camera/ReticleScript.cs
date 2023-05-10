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
    private IThreat _threat;
    Book book;
    RaycastHit hit;

    SoundManager soundManager;
    private bool hasPlayedSound = false;

    private void Start()
    {
        //cache the book instance
        book = Book.instance;

        soundManager = SoundManager.instance;

        GameEvents.instance.onCameraOpened += ResetDiscoveryUI;
    }

    public void ResetDiscoveryUI()
    {
        discoveryStatusImage.sprite = undiscoveredSprite;
        discoveryStatus.SetText("Undiscovered");
    }

    public void Discovered()
    {
        if (hit.collider != null)
        {
            //OLD METHOD
            //hit.collider.gameObject.SendMessage("Discovered");

            //NEW METHOD (OMSIMIZED)
            if (_animal != null)
            {
                _animal.Discovered();
            }

            if(_threat != null)
            {
                Debug.Log("THREAT IS NOT NULL");
                _threat.Discovered();
            }
        }
    }
    public void Ray()
    {
        Debug.DrawRay(transform.position, transform.forward * 200f, Color.red);

        bool shouldPlaySound = false;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 50f, layerMask))// && hit.transform.gameObject.CompareTag("NPC"))
        {
            reticle.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            reticle.sprite = discoveredCrosshair;

            //enable panel
            discoveryPanel.SetActive(true);

            //check discovery status

            //if raycast is animal
            _animal = hit.collider.GetComponent<IAnimal>();

            if (_animal != null)
            {
                for (int i = 0; i < book.photosInventory.Count; i++)
                {
                    if (book.photosInventory[i].name == _animal.animalName)
                    {
                        discoveryStatusImage.sprite = book.photosInventory[i].polaroidPhoto;
                        discoveryStatus.SetText(_animal.animalName);
                        _isDiscovered = true;

                        shouldPlaySound = true;
                    }
                }
            }

            else
            {
                _threat = hit.collider.GetComponent<IThreat>();
                if (_threat != null)
                {

                    for (int i = 0; i < book.photosThreat.Count; i++)
                    {
                        if (book.photosThreat[i].threatName == _threat.threatName)
                        {
                            discoveryStatusImage.sprite = book.photosThreat[i].threatPicture;
                            discoveryStatus.SetText(_threat.threatName);
                            _isDiscovered = true;

                            shouldPlaySound = true;
                        }
                    }
                }
            }

            if (_isDiscovered == false)
            {
                discoveryStatusImage.sprite = undiscoveredSprite;
                discoveryStatus.SetText("Undiscovered");

                shouldPlaySound = true;
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

            hasPlayedSound = false;
        }

        if (shouldPlaySound && !hasPlayedSound)
        {
            soundManager.PlaySoundFromClips(8);
            hasPlayedSound = true;
        }
    }

}
