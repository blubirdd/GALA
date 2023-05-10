using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    #region Singleton

    public static HintUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of HintUI found");
            return;
        }

        instance = this;
    }
    #endregion
    [Header("References")]
    public GameObject questStatusHintCanvas;
    public TextMeshProUGUI questStatusText;

    public void FireQuestHint()
    {
        questStatusHintCanvas.SetActive(true);
        questStatusText.text = "Put out the fires before the timer runs out!";
    }

    public void DisableHint()
    {
        questStatusHintCanvas.SetActive(false);
        questStatusText.text = "";
    }
}
