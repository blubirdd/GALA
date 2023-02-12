using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues/Dialogue")]
public class DialogueSO : ScriptableObject
{
    new public string name = "Name";

    [TextArea(3, 10)]
    [NonReorderable]public string[] sentences;

    [TextArea(3, 10)]
    [NonReorderable] public string[] isTalkedSentences;

}
