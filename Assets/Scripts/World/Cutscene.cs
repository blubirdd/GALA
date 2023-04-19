using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour, IDataPersistence
{
    public string ID;
    public string locationName;
    public string placeName;
    public GameObject discoveryUI;
    [SerializeField] private bool isDone = false;

    private QuestNew quest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

    [Header("Object to disable")]
    [SerializeField] private GameObject[] objectToDisable;


    [Header("If change Fog and Ambient Light")]
    [SerializeField] private bool changeAmbientLight;
    [SerializeField] private Color dayAmbientLight;

    [SerializeField] private Color nightAmbientLight;

    [Header("Fog")]
    [SerializeField] private bool changeFogColor;
    [SerializeField] private Color dayFogColor;
    [SerializeField] private Color nightFogColor;

    Character character;
    void Start()
    {
        character = GetComponent<Character>();
    }
    void SpawnDiscoveryUI()
    {
        GameObject go = Instantiate(discoveryUI);
        go.GetComponent<Discovery>().SetName(locationName);
        go.GetComponent<Discovery>().SetPlaceName(placeName);

        isDone = true;


    }
    void OnTriggerEnter(Collider collision)
    {

        if(collision.transform.tag == "Player" && !isDone)
        {
            GameEvents.instance.CutscenePlay();

            //ui
            SpawnDiscoveryUI();


            //turn off joystick
            UIManager.instance.DisablePlayerMovement();

            StartCoroutine(WaitToAssignQuest());


            //if(objectToDisable != null)
            //{
            //    objectToDisable.SetActive(false);
            //}

            foreach (var objects in objectToDisable)
            {
                objects.SetActive(false);
            }
        }
    }

    IEnumerator WaitToAssignQuest()
    {



        yield return new WaitForSeconds(7f);
        if (character != null)
        {
            TalkEvents.CharacterApproach(character);
            yield return new WaitForSeconds(4f);
        }
        if (questType != null)
        {
            //Assign Quest
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
        }


    }

    public void LoadData(GameData data)
    {
        data.cutsceneTriggered.TryGetValue(ID, out isDone);
    }

    public void SaveData(GameData data)
    {
        if (data.cutsceneTriggered.ContainsKey(ID))
        {
            data.cutsceneTriggered.Remove(ID);
        }

        data.cutsceneTriggered.Add(ID, isDone);
    }


    //IEnumerator WaitForSecondsThenDestroy()
    //{
    //    yield return new WaitForSeconds(3f);

    //    GameEvents.instance.CameraClosed();

    //    //ui
    //    UIManager.instance.EnablePlayerMovement();

    //    Destroy(this.gameObject);
    //}
}
