using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonAimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;
 
    private Camera _mainCam;

    //firable
    [SerializeField] private LayerMask collectableMask;
    RaycastHit hit;
    private GameObject lastCollectable;
    private Outline lastOutline;

    [Header("Hook")]
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform fishingHookEnd;
    

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if(starterAssetsInputs.aim)
        {
            //to change
            UIVirtualTouchZone.sensitivity = 0.7f;

            aimVirtualCamera.gameObject.SetActive(true);
  

            Quaternion cameraRotation = _mainCam.transform.rotation;

            // Rotate the player with camera rotation
            transform.rotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);


            //raycast

            if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out hit, 30f, collectableMask))
            {
                GameObject currentCollectable = hit.collider.gameObject;

                // If the current collectable is different from the last one, disable the outline of the last one and enable the outline of the new one
                if (currentCollectable != lastCollectable)
                {
                    if (lastOutline != null)
                    {
                        lastOutline.enabled = false;
                    }

                    Outline currentOutline = currentCollectable.GetComponent<Outline>();
                    if (currentOutline != null)
                    {
                        currentOutline.enabled = true;
                        lastOutline = currentOutline;
                    }

                    // Update the last collectable to the current one
                    lastCollectable = currentCollectable;
                }
            }
            else
            {
                // If no collectable is hit by the raycast, disable the outline of the last one (if it exists)
                if (lastOutline != null)
                {
                    lastOutline.enabled = false;
                    lastOutline = null;
                }

                // Reset the last collectable
                lastCollectable = null;
            }


           
 

        }

        else
        {
            if(aimVirtualCamera.gameObject.activeSelf)
            {
                aimVirtualCamera.gameObject.SetActive(false);
                UIVirtualTouchZone.sensitivity = 3f;

            }

            else
            {
                return;
            }

        }
    }

    public void Hook()
    {
        StartCoroutine(WaitForAnimation());
        IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(1f);
            var hook = Instantiate(hookPrefab, _mainCam.transform.position + _mainCam.transform.forward, _mainCam.transform.rotation);

            hook.GetComponent<Hook>().caster = fishingHookEnd;
        }

    }



}
