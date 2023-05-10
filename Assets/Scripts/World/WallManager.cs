using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    #region Singleton

    public static WallManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of WallManager found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject swampForestWall;

}
