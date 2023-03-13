using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TouchPhase = UnityEngine.TouchPhase;

public class DestroyOnTouch : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

public void DestroySnake()
{
    Destroy(gameObject); 
    Debug.Log(gameObject + " hasn't beed destroyed yet!"); 
}
}
