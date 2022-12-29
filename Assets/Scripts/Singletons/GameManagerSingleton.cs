using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    static GameManagerSingleton instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
