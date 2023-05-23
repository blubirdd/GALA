using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class Inventory : MonoBehaviour, IDataPersistence
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private ItemDatabaseObject database;

    public delegate void OnItemChange();
    public OnItemChange OnItemChangedCallback;

    //communicate with collectiongoal
    public delegate void OnItemAdded(Item item);
    public static event OnItemAdded OnItemAddedCallback;

    //communicate with usegoal
    public delegate void OnItemUsed(Item item);
    public static event OnItemUsed OnItemUsedCallback;


    //  public List<Item> items = new List<Item> ();

    //savable inventory
    [NonReorderable] public List<InventorySave> container = new List<InventorySave>();

    //save
    public int goldCoins;
    public int naturePoints;

    public Item coins;
    public Item seeds;

    [Header("New Discovery")]
    public ItemDiscovery itemDiscovery;
    [Header("Newspaper Discovery")]
    public NewpaperDiscovery newpaperDiscovery;

    [Header("Toolbar")]
    public GameObject[] toolbarItems;

    private void Start()
    {
        //set toolbar items to true once tutorial is done
        if (Task.instance.tasksCompeleted.Contains("QuestIntroPart2"))
        {
            foreach (var item in toolbarItems)
            {
                item.SetActive(true);
            }
        }

        else
        {
            foreach (var item in toolbarItems)
            {
                item.SetActive(false);
            }
        }

        if(Player.instance.chickenGameScore > 0)
        {
            Add(seeds, Player.instance.chickenGameScore, false);
        }
    }

    public void EnableToolBarItems()
    {
        //StartCoroutine(WaitToInitializeToolbarItems());
        //IEnumerator WaitToInitializeToolbarItems()
        //{
        //    yield return new WaitForEndOfFrame();

            foreach (var item in toolbarItems)
            {
                item.SetActive(true);
            }
        //}

    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset",typeof(ItemDatabaseObject));

#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }

    //old
    //public void Add (Item item)
    //{

    //    if(!item.isDefaultItem)
    //    {
    //        ItemPickedUp(item);

    //        if (items.Contains(item))
    //        {
    //            item.itemAmount++;
    //        }
    //        else
    //        { 
    //           items.Add(item);
    //           container.Add(new InventorySave(database.getId[item], item));
    //        }

    //        if (OnItemChangedCallback != null)
    //        {
    //            OnItemChangedCallback.Invoke();
    //        }
            
    //    }


    //}


    public void Add(Item item, int _amount, bool triggerDiscovery)
    {
        
        //trigger the collectiongoal event
        ItemPickedUp(item);

        if (item.displayDiscovery && triggerDiscovery)
        {
            bool isFound = false;
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item)
                {
                    isFound = true;
                }
            }

            if (!isFound)
            {
                itemDiscovery.NewItemDiscovered(item.icon, item.name, item.itemDescription, true);
                isFound = false;
            }
        }

        if (!item.isDefaultItem)
        {
            
            for(int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item)
                {
                    container[i].AddAmount(_amount);

                    //trigger event
                    if (OnItemChangedCallback != null)
                    {
                        OnItemChangedCallback.Invoke();
                    }
                    return;
                }
             
            }
            container.Add(new InventorySave(database.getId[item], item, _amount));

            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }

        }

    }

    public void RemovePoints(int points)
    {
        if(naturePoints > 0)
        naturePoints = +points;
    }

    public void AddCoins(int coins)
    {
        goldCoins += coins;
    }

    public void RemoveCoins(int coins)
    {
        if(goldCoins >0)
        goldCoins -= coins;
    }


    //public void Remove(Item item)
    //{

    //    items.Remove(item);


    //    if (OnItemChangedCallback != null)
    //    {
    //        OnItemChangedCallback.Invoke();
    //    }
    //}

    public void Remove(Item item)
    {

        container.RemoveAll(container => item == container.item);

        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public void DecreaseItemAmountByOne(Item item)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                container[i].amount -= 1;

                if (container[i].amount <= 0 && container[i].item.displayQuantity)
                {
                    //remove item
                    Remove(item);
                }

                Debug.Log(container[i].amount);
                if (OnItemChangedCallback != null)
                {
                    OnItemChangedCallback.Invoke();
                }

                return;
            }
        }
    }

    //COLLECTION GOAL
    public void ItemPickedUp(Item item)
    {
        if(OnItemAddedCallback != null)
        {
           OnItemAddedCallback(item);
        }
    }

    //USE GOAL
    public void ItemUsed(Item item)
    {
        if(OnItemUsedCallback != null)
        {
            OnItemUsedCallback(item);
        }
    }


    [System.Serializable]
    public class InventorySave
    {
        public int ID;
        public Item item;
        public int amount = 1;

        public InventorySave(int _id, Item _item, int _amount)
        {
            ID = _id;
            item = _item;
            amount = _amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }


    public void LoadData(GameData data) 
    {
        foreach (KeyValuePair<int, InventorySave> keyValuePair in data.itemsCollected)
        {
            //this method does not work
            //container[keyValuePair.Value].item = keyValuePair.Key.item;
            // container.Add(new InventorySave(keyValuePair.Value, keyValuePair.Key.item));

            //this method does not work because instance id is not reliable for serializing 
            //Add(keyValuePair.Value.item, keyValuePair.Value.amount);

            //this method works by getting the item using the id
            Add(database.getItem[keyValuePair.Value.ID], keyValuePair.Value.amount, false);
        }

        goldCoins = data.coins;
        this.naturePoints = data.naturePoints; 

    }

    public void SaveData(GameData data)
    {
        //clear current dictionary
        data.itemsCollected.Clear();

        //Add inventory to gamedata inventory dictionary
        for (int i = 0; i < container.Count; i++)
        {
            data.itemsCollected.Add(container[i].ID, container[i]);
        }

        data.coins = goldCoins;
        data.naturePoints = this.naturePoints;

    }

    public void UnlockAllItems()
    {
        for (int i = 0; i < database.items.Length; i++)
        {
            Add(database.getItem[i], 1, false);
        }
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }

        GamesUnlockManager.instance.UnlockAll();
    }
}
