using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameStart : MonoBehaviour, IInteractable
{
    //[SerializeField] private ItemDatabaseObject database;
    /*[SerializeField] private string id;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    */

    //    private void OnEnable()
    //    {
    //#if UNITY_EDITOR
    //        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));

    //#else
    //        database = Resources.Load<ItemDatabaseObject>("Database");
    //#endif
    //    }

    [SerializeField] private string _prompt = "Pick up ";
    [SerializeField] private Sprite _icon;



    public Item item;


    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }

    ThirdPersonController instance;
    PopupWindow popupWindow;
    //waypoint
    [Header("If has waypoint on pickup, else leave null")]
    [SerializeField] private Transform _waypointTransform;

    void Start()
    {
        InteractionPrompt = _prompt;
        icon = _icon;
        instance = ThirdPersonController.instance;
        popupWindow = PopupWindow.instance;


    }
    public bool Interact(Interactor interactor)
    {
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("Mole Minigame");
        return true;
    }

    public void StartMoleMinigame()
    {
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("Mole Minigame");
    }
}
