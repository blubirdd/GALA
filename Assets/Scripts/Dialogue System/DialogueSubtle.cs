using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueSubtle
{

    [TextArea(3, 10)]
    [NonReorderable] public string[] sentences;
}
