using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InteractionPromptUI : MonoBehaviour
{

    [SerializeField] private GameObject interactionButton;
    [SerializeField] private TextMeshProUGUI _prompText;
    [SerializeField] private Image _icon;
    void Start()
    {
       interactionButton.SetActive(false);
    }


    public bool isDisplayed = false;
    public void Setup(string promptText, Sprite icon)
    {
        _prompText.text = promptText;
        _icon.sprite = icon;

        interactionButton.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        isDisplayed = false;
        interactionButton.SetActive(false);
    }
}
