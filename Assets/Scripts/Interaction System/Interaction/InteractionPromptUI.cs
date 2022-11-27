using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{

    [SerializeField] private GameObject interactionButton;
    [SerializeField] private TextMeshProUGUI _prompText;
    void Start()
    {
       interactionButton.SetActive(false);
    }


    public bool isDisplayed = false;
    public void Setup(string promptText)
    {
        _prompText.text = promptText;
        interactionButton.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        isDisplayed = false;
        interactionButton.SetActive(false);
    }
}
