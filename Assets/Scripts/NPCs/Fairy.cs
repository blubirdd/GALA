using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class Fairy : MonoBehaviour,  IInteractable, ICharacter, IDataPersistence 
{

    [Header("Unique ID")]
    [SerializeField] private string id;

    [Header("Interaction")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    [SerializeField] private DialogueTrigger _dialogue;
    [SerializeField] private DialogueTrigger _isTalkedDialogue;

    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    [Header("Quest")]
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;
    [SerializeField] private bool isTalked = false;

    [Header("Quest Marker")]
    public GameObject questMarker;
    private QuestNew quest { get; set; }
    public string npcName { get; set; }

    Rigidbody rb;



    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;

        npcName = _dialogue.name;

        if (isTalked == true)
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
    }


    public bool Interact(Interactor interactor)
    {
        //trigger dialogue
        if (isTalked == false)
        {
            _dialogue.TriggerDialogue();

            //EVENT TRIGGER
            TalkEvents.CharacterApproach(this);

            StartCoroutine(AcceptQuest());
        }


        return true;
    }

    IEnumerator AcceptQuest()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        AssignQuest();

    }

    void AssignQuest()
    {
        if (isTalked == false)
        {
            isTalked = true;
        }
        DisableQuestMarker();
    }

    void DisableQuestMarker()
    {
        questMarker.SetActive(false);

        StartCoroutine(FadeOut());
    }

    private bool flyaway = false;
    void Update()
    {
        //if(flyaway == true)
        //{

        //}

    }

    IEnumerator FadeOut()
    {

        flyaway = true;
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + Vector3.up * Time.deltaTime * 3f;
        transform.position = newPosition;

        ParticleManager.instance.SpawnPuffParticle(transform.position);

        _isTalkedDialogue.TriggerIsTalkedDialogue();

        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log(this + "Quest New Assigned");


        Destroy(this.gameObject);
    }

    public void LoadData(GameData data)
    {
        data.NPCsTalked.TryGetValue(id, out isTalked);
    }

    public void SaveData(GameData data)
    {
        if (data.NPCsTalked.ContainsKey(id))
        {
            data.NPCsTalked.Remove(id);
        }

        data.NPCsTalked.Add(id, isTalked);
    }
}
