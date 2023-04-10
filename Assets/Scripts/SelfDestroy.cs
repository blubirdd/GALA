using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void DestroyThisObject()
    {
        Destroy(gameObject);
        UIManager.instance.EnablePlayerMovement();
        Time.timeScale = 1;
    }
}
