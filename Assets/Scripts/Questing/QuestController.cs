using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    #region Singleton

    public static QuestController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuestController found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject questManager;
    private QuestNew quest { get; set; }
    [SerializeField] private GameObject waypointsParent;

    [Header("Quest Locations")]
    [Header("Grassland")]
    public Transform cureTamarawQuestLocation;


    [Header("River")]
    public Transform fireQuestLocation;

    [Header("Rainforest")]
    public Transform photoEagleQuestLocation;

    [Header("Items to add")]
    public Item bucketItem;
    public Item medkitItem;

    [Header("Swamp")]
    public Transform swampLocation;
    public QuestNPC swampQuestNPC;

    [Header("Swamp Enclosure")]
    public Transform swampEnclosureLocation;
    public QuestNPC swampEnclosureQuestNPC;

    [Header("Rainforest Start")]
    public Transform rainForestEntranceLocation;
    public QuestNPC rainforestFairy;

    [Header("Beach")]
    public Transform beachLocation;

    [Header("Forest Hunters")]
    public Transform hunterRespawnPoint;

    [Header("Village")]
    public Transform villageRespawnPoint;
    public void TransferPlayer(Transform location)
    {
        ThirdPersonController.instance.gameObject.SetActive(false);
        ThirdPersonController.instance.transform.position = location.position;
        ThirdPersonController.instance.gameObject.SetActive(true);
    }

    public void ClearTasks()
    {
        Task.instance.tasks.Clear();
    }

    public void AddNecessaryTask()
    {
        Task.instance.tasksCompeleted.Add("QuestIntroPart2");
        Task.instance.tasksCompeleted.Add("QuestIntroPart1");
    }
    public void AcceptQuestFireQuestControl()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();

        Task.instance.tasksCompeleted.Remove("QuestPutOutFire");
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestPutOutFire"));
        Inventory.instance.Add(bucketItem, 1, false);
        TransferPlayer(fireQuestLocation);
        PlayerLocationManager.currentLocation = "River";

        PlayerLocationManager.instance.SetCurrentLocationActive();

        //set time to night
        TimeController.instance.SetTimeOfDay(20);
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestTamarawQuestControl()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestUseMedkit2"));

        Inventory.instance.Add(medkitItem, 1, false);
        TransferPlayer(cureTamarawQuestLocation);

        PlayerLocationManager.currentLocation = "Grassland";

        PlayerLocationManager.instance.SetCurrentLocationActive();


        TimeController.instance.SetTimeOfDay(10);
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestEagleQuestControl()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestPhotographEagle"));

        TransferPlayer(photoEagleQuestLocation);
        PlayerLocationManager.currentLocation = "Rainforest";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestSwampLevel()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestTalkSwampResident"));

        TransferPlayer(swampLocation);
        PlayerLocationManager.currentLocation = "Swamp";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);

        swampQuestNPC.isTalked = false;
        swampQuestNPC.isCompleted = false;
        swampQuestNPC.gameObject.layer = LayerMask.NameToLayer("Interactable");
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestSwampEnclosureLevel()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestEnterCrocSanctuary"));

        TransferPlayer(swampEnclosureLocation);
        PlayerLocationManager.currentLocation = "Swamp";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);

        swampEnclosureQuestNPC.isTalked = false;
        swampEnclosureQuestNPC.isCompleted = false;
        swampEnclosureQuestNPC.gameObject.layer = LayerMask.NameToLayer("Interactable");
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestRainforest()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestTalkRainforestFairy"));

        TransferPlayer(rainForestEntranceLocation);
        PlayerLocationManager.currentLocation = "Rainforest";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);

        rainforestFairy.isTalked = false;
        rainforestFairy.isCompleted = false;
        rainforestFairy.gameObject.layer = LayerMask.NameToLayer("Interactable");
        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestBeachPushDugong()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestInspectDugong"));

        TransferPlayer(beachLocation);
        PlayerLocationManager.currentLocation = "River";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);

        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestForestHunters()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestCollectKeys"));

        TransferPlayer(hunterRespawnPoint);
        PlayerLocationManager.currentLocation = "Rainforest";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(20);

        ClearCompletedQuestDatabase();
    }

    public void AcceptQuestVillageChicken()
    {
        ClearAllQuest();
        ClockManager.instance.DisableClock();
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType("QuestFeedChicken"));

        TransferPlayer(villageRespawnPoint);
        PlayerLocationManager.currentLocation = "Village";
        PlayerLocationManager.instance.SetCurrentLocationActive();

        TimeController.instance.SetTimeOfDay(10);

        ClearCompletedQuestDatabase();

        Task.instance.tasksCompeleted.Add("QuestPhotoChicken");
    }

    public void ClearCompletedQuestDatabase()
    {
        Task.instance.tasksCompeleted.Clear();
        AddNecessaryTask();
        Inventory.instance.EnableToolBarItems();
    }

    public void ClearAllQuest()
    {
        DestroyAllWaypoints();
        ClearTasks();
        ClearAllComponents();
    }

    public void DestroyAllWaypoints()
    {
        int childCount = waypointsParent.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(waypointsParent.transform.GetChild(i).gameObject);
        }
    }

    public void ClearAllComponents()
    {
        QuestNew[] components = questManager.GetComponents<QuestNew>();
        for (int i = 0; i < components.Length; i++)
        {
            // Destroy the component
            Destroy(components[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
