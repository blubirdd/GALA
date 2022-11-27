using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{


    private GameObject triggeringNpc;
    private bool triggering;
    public GameObject npcText;
    // private int talkValue = 0;
   // public OLDDialogueScript script;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggering == true)// && script.dialogueDone == false)
        {
            //trigger something
           
           // FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
            npcText.SetActive(true);
            
        }
        else
        {   //dont trigger
            npcText.SetActive(false);
        }
        
    }

    public void TriggerDialogue()
    {

    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering=true;
            triggeringNpc = other.gameObject;
           
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering=false;
            triggeringNpc = null;

            //enable dialogue again
          //  script.dialogueDone = false;
        }
        
    }
}
