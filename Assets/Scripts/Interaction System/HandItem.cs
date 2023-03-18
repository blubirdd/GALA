using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItem : MonoBehaviour
{
    public Equipment handItem;

    [Header("If Throwable, else leave null")]
    public GameObject handItemPrefab;

    public bool disableOnthrow = true;
}
