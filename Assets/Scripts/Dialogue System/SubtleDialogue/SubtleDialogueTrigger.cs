using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtleDialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

   // public DialogueSO dialogueSO;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueSystem>().StartSubtleDialogue(dialogue);
    }


}
