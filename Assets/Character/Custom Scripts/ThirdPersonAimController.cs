using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ThirdPersonAimController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if(starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);

            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Rotate the player with camera rotation
            transform.rotation = Quaternion.Euler(0f, cameraRotation.eulerAngles.y, 0f);

        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);

        }
    }



}
