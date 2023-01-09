using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public Dialogue isTalkedDialogue;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }

    public void TriggerIsTalkedDialogue()
    {
        if (isTalkedDialogue != null)
        {
            FindObjectOfType<DialogueSystem>().StartDialogue(isTalkedDialogue);
        }

    }



}
