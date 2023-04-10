using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewpaperDiscovery : MonoBehaviour
{
    [SerializeField] private GameObject newsPaperDiscoveryPanel;

    [SerializeField] private GameObject spinningWheel;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI newsPaperDescriptionText;

    UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.instance;
        spinningWheel.transform.DORotate(new Vector3(0f, 0f, -360), 40, RotateMode.FastBeyond360)
       .SetLoops(-1, LoopType.Restart)
       .SetEase(Ease.Linear);
    }

    public void NewItemDiscovered(Sprite icon, string name, string description)
    {
        uiManager.DisablePlayerMovement();
        newsPaperDiscoveryPanel.SetActive(true);
        image.sprite = icon;
        newsPaperDescriptionText.text = description;

    }

    public void ClosePanel()
    {
        newsPaperDiscoveryPanel.SetActive(false);

        //if (forItem)
        //{
        //    uiManager.EnablePlayerMovement();
        //}
        //forItem = false;
    }

}
