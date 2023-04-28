using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TouchPhase = UnityEngine.TouchPhase;

public class DestroyOnTouch : MonoBehaviour
{

     public SpriteRenderer spriteRenderer;
    EggGameManager eggGameManager;


    private void Start()
    {
        eggGameManager = EggGameManager.instance;
    }
    public void DestroySnake()
    {

        Destroy(gameObject); 
    }


}
