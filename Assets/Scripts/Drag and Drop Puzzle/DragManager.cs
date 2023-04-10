using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    #region Singleton

    public static DragManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DragManager found");
            return;
        }

        instance = this;
    }
    #endregion

    public DragSystem[] dragSystems;

    public GameObject medkitCanvas;
    public void EnableMedkit(DragSystem dragSystem)
    {
        for (int i = 0; i < dragSystems.Length; i++)
        {
            if (dragSystems[i] == dragSystem)
            {
                dragSystem.isCorrect = true;
                medkitCanvas.SetActive(true);
            }
        }
    }
}
