using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class DialogueSystem : MonoBehaviour
{

    public static bool dialogueEnded = false;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject controlsCanvas;
    public GameObject dialogueCanvas;


   
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }


    public void StartDialogue(Dialogue dialogue)
    {
     
        dialogueEnded = false;

        //disable contrls
        controlsCanvas.SetActive(false);

        dialogueCanvas.SetActive(true);

        //
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
  

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();


    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //anim speed
            yield return new WaitForSeconds(0.05f);
            yield return null;
        }
    }

    public void EndDialogue()
    {

        animator.SetBool("isOpen", false);

        //enable controls
        controlsCanvas.SetActive(true);
        
        dialogueEnded = true;

     
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }


}
