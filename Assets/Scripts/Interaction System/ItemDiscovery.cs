using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ItemDiscovery : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject itemDiscoveryPanel;

    [SerializeField] private GameObject spinningWheel;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.instance;
         spinningWheel.transform.DORotate(new Vector3(0f, 0f, -360), 10, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);
    }

    public void NewItemDiscovered(Sprite icon, string name, string description)
    {
        uiManager.DisablePlayerMovement();
        itemDiscoveryPanel.SetActive(true);
        image.sprite = icon;
        itemNameText.text = name;
        itemDescriptionText.text = description;
    }

    public void ClosePanel()
    {
        itemDiscoveryPanel.SetActive(false);
        uiManager.EnablePlayerMovement();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
