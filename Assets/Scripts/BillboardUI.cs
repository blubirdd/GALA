using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BillboardUI : MonoBehaviour
{

    private Camera _mainCam;

    [SerializeField] private bool bounce;

    [SerializeField] private float duration = 2f;
    [SerializeField] private float jumpHeight = 0.2f;
    [SerializeField] private int jumpCount = 1;

    [Header("If bounce, else leave null")]
    [SerializeField]private Transform transformOfImage;
    void Start()
    {
        _mainCam = Camera.main;
        
    }


    private void LateUpdate()
    {
       var rotation = _mainCam.transform.rotation;
       transform.LookAt(worldPosition: transform.position + rotation * Vector3.forward, rotation * Vector3.up);

    }

    private void OnEnable()
    {
        if (bounce)
        {
            transformOfImage.DOKill(); // Stop any previous tween animations
            transformOfImage.DOJump(new Vector3(transformOfImage.position.x, transformOfImage.position.y + jumpHeight, transformOfImage.position.z),
                jumpHeight, jumpCount, duration)
                .SetLoops(-1, LoopType.Yoyo); // Bounce up and down continuously
        }
    }

    private void OnDisable()
    {
        transformOfImage.DOKill(); // Stop the tween animation when the UI element is disabled
    }



}
