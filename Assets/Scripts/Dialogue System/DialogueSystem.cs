using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using DG.Tweening;
using Cinemachine;

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
    [Header("UI")]
    public Button nextSentenceButton;
    public CanvasGroup continueButton;
    public float fadeDuration = 5f; // The duration of the fade animation
    public float pauseDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //continueButton.alpha = 0f;
        //Sequence fadeInOut = DOTween.Sequence()
        //    .Append(continueButton.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad))
        //    .AppendInterval(pauseDuration) 
        //    .Append(continueButton.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad))
        //    .AppendInterval(pauseDuration) 
        //    .SetLoops(-1, LoopType.Restart);

        //// Start the fade in and out sequence
        //fadeInOut.Play();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        CinemachineTargetGroup.instance.m_Targets[1].target = null;

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

        nextSentenceButton.gameObject.SetActive(false);

        StartCoroutine(WaitForSecondsToDisplay());
        IEnumerator WaitForSecondsToDisplay()
        {
            yield return new WaitForSeconds(1f);
            nextSentenceButton.gameObject.SetActive(true);
            continueButton.alpha = 0f;
            continueButton.DOFade(1f, 1f);
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        for (int letterIndex = 0; letterIndex < sentence.Length; letterIndex += 2)
        {
            dialogueText.text += sentence[letterIndex];
            if (letterIndex + 1 < sentence.Length)
            {
                dialogueText.text += sentence[letterIndex + 1];
            }
            //  yield return new WaitForSeconds(0.01f);
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

    IEnumerator EnumDisplaySubtleNextSentence()
    {
        while (true)
        {
            DisplaySubtleNextSentence();
            Debug.Log("DISPLAY NEXT SENTENCE");
            yield return new WaitForSeconds(3f);

            if(sentences.Count == 0)
            {
                EndSubtleDialogue();
                yield break;
            }
        }
    }

    public void DisplaySubtleNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        StopCoroutine(TypeSubtleSentence(sentence));
        StartCoroutine(TypeSubtleSentence(sentence));
    }


    IEnumerator TypeSubtleSentence(string sentence)
    {
        subtleDialogueText.text = "";
        subtleDialogueText.DOFade(0f, 0f); // set the initial alpha to 0
        subtleDialogueText.text = sentence;
        subtleDialogueText.DOFade(1f, 1.5f);

       
        yield return null;
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
