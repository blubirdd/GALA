using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    public string locationName;
    public GameObject discoveryUI;
    bool isDone = false;

    private QuestNew quest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;


    void SpawnDiscoveryUI()
    {
        GameObject go = Instantiate(discoveryUI);
        go.GetComponent<Discovery>().SetName(locationName);

        isDone = true;
    }
    void OnTriggerEnter(Collider collision)
    {

        if(collision.transform.tag == "Player" && !isDone)
        {
            GameEvents.instance.CutscenePlay();

            //ui
            UIManager.instance.DisablePlayerMovement();

            SpawnDiscoveryUI();
            quest = (QuestNew)quests.AddComponent(System.Type.GetType(questType));
            Debug.Log("Cutscene playing");

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
