using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{

    static PlayerSingleton instance;


    private void Awake()
    {
        if(instance != null)
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
