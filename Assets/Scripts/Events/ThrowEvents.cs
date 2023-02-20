using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEvents : MonoBehaviour
{
    public delegate void ThrowEventsHandler(IThrowable item);

    public static event ThrowEventsHandler onItemThrown;

    public static void ItemThrown(IThrowable item)
    {
        if(onItemThrown != null)
        {
            onItemThrown(item);
        }
    }
}
