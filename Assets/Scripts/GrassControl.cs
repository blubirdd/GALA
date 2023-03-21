using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassControl : MonoBehaviour
{
    public GameObject grass;

    public void EnableDisableGrass()
    {
        grass.SetActive(!grass.activeSelf);
    }
}
