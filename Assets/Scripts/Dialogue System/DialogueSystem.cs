using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using DG.Tweening;
using Cinemachine;
using System.Linq;
using System;

public class DialogueSystem : MonoBehaviour
{

    public static bool dialogueEnded = false;


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    public Animator animator;

    private Queue<string> sentences;
    private Queue<string> subtleSentences;
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

    [Header("Audio")]
    [SerializeField] private AudioClip[] dialogueSoundClips;
    private AudioSource audioSource;
    [SerializeField] private bool stopAudioSource;
    [Range(1,5)]
    [SerializeField] private int frequencyLevel = 3;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;

    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 2f;

    [Range(0, 1)]
    [SerializeField] private float _volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        subtleSentences = new Queue<string>();
        //continueButton.alpha = 0f;
        //Sequence fadeInOut = DOTween.Sequence()
        //    .Append(continueButton.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad))
        //    .AppendInterval(pauseDuration) 
        //    .Append(continueButton.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad))
        //    .AppendInterval(pauseDuration) 
        //    .SetLoops(-1, LoopType.Restart);

        //// Start the fade in and out sequence
        //fadeInOut.Play();

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume = _volume;
    }


    public void StartDialogue(Dialogue dialogue)
    {
        //CinemachineTargetGroup.instance.m_Targets[1].target = null;

        dialogueEnded = false;

        GameEvents.instance.DialogueStarted();

        //disable contrls
        controlsCanvas.SetActive(false);

        dialogueCanvas.SetActive(true);
        
       
        //
        animator.SetBool("isOpen", true);

        if(dialogue.name == "Player")
        {
            nameText.text = Player.playerName;
        }
        else
        {
            nameText.text = dialogue.name;
        }

  

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
            yield return new WaitForSeconds(0.5f);
            nextSentenceButton.gameObject.SetActive(true);
            continueButton.alpha = 0f;
            continueButton.DOFade(1f, 0.5f);
        }
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        int currentDisplayedCharacterCount = 0;
        for (int letterIndex = 0; letterIndex < sentence.Length; letterIndex += 2)
        {
            dialogueText.text += sentence[letterIndex];
            currentDisplayedCharacterCount ++;
            if (letterIndex + 1 < sentence.Length)
            {
                dialogueText.text += sentence[letterIndex + 1];
                PlayDialogueSound(currentDisplayedCharacterCount);

            }
            //  yield return new WaitForSeconds(0.01f);
            yield return null;
        }
       
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % frequencyLevel == 0) // Play the audio clip every x characters
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            int randomIndex = UnityEngine.Random.Range(0, dialogueSoundClips.Length);
            AudioClip soundClip = dialogueSoundClips[randomIndex];
            //audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(soundClip);
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
        if (subtleDialogueCanvas.activeSelf)
        {
            // If the subtle dialogue is already active, check if it's displaying the same dialogue
            if (dialogue.sentences.SequenceEqual(subtleSentences))
            {
                // If it is, do nothing
                return;
            }
            else
            {
                // If it's not, stop displaying the current dialogue and start displaying the new one
                StopAllCoroutines();
                EndSubtleDialogue();
            }
        }

        subtleDialogueCanvas.SetActive(true);
        subtleSentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            subtleSentences.Enqueue(sentence);
        }

        StartCoroutine(EnumDisplaySubtleNextSentence());
    }

    IEnumerator EnumDisplaySubtleNextSentence()
    {
        while (true)
        {
            DisplaySubtleNextSentence();
            Debug.Log("DISPLAY NEXT SENTENCE");
            yield return new WaitForSeconds(5f);

            if(subtleSentences.Count == 0)
            {
                EndSubtleDialogue();
                yield break;
            }
        }
    }

    public void DisplaySubtleNextSentence()
    {
        if (subtleSentences.Count == 0)
        {
            return;
        }
        string sentence = subtleSentences.Dequeue();
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
