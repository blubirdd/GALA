using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject objectiveListCanvas;

    public void OpenObjectiveList()
    {
        objectiveListCanvas.SetActive(!objectiveListCanvas.activeSelf);
    }
}
