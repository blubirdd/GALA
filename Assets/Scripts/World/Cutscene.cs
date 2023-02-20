using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    public string locationName;
    public string placeName;
    public GameObject discoveryUI;
    bool isDone = false;

    private QuestNew quest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;

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

            if(questType != null)
            {
                //Assign Quest
                quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
            }

            Debug.Log("Cutscene playing");

            //turn off joystick
            UIManager.instance.DisablePlayerMovement();

            //StartCoroutine(WaitForSecondsThenDestroy());
        }
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
