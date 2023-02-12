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

    [Header("Subtle Dialogue")]
    public GameObject subtleDialogueCanvas;
    public TextMeshProUGUI subtleDialogueText;
    //[SerializeField] private float _textSpeed;


   
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }


    public void StartDialogue(Dialogue dialogue)
    {


        dialogueEnded = false;

        GameEvents.instance.DialogueStarted();

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
           // yield return new WaitForSeconds(_textSpeed);
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

    //////////////SUBTLE DIALOGUE
    public void StartSubtleDialogue(Dialogue dialogue)
    {
        subtleDialogueCanvas.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        StartCoroutine(EnumDisplaySubtleNextSentence());
    }

    public void DisplaySubtleNextSentence()
    {
        if (sentences.Count == 0)
        {
            StopCoroutine(EnumDisplaySubtleNextSentence());
            EndSubtleDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopCoroutine(TypeSubtleSentence(sentence));
        StartCoroutine(TypeSubtleSentence(sentence));
    }

    IEnumerator EnumDisplaySubtleNextSentence()
    {
        while(true)
        {
            DisplaySubtleNextSentence();
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator TypeSubtleSentence(string sentence)
    {
        subtleDialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            subtleDialogueText.text += letter;

            yield return null;
        }
    }
    public void EndSubtleDialogue()
    {
        Debug.Log("Subtle Dialogue End");
        subtleDialogueCanvas.SetActive(false);
    }
     


    // Update is called once per frame
    void Update()
    {
        
    }


}
