using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Inventory;


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

    public delegate void OnItemChange();
    public OnItemChange OnItemChangedCallback;

    //communicate with collectiongoal
    public delegate void OnItemAdded(Item item);
    public static event OnItemAdded OnItemAddedCallback;


    public List<Item> items = new List<Item> ();

    public int naturePoints;

    private void Start()
    {
        
    }


    public void Add (Item item)
    {

        if(!item.isDefaultItem)
        {
            ItemPickedUp(item);

            if (items.Contains(item))
            {
                item.itemAmount++;
            }
            else
            { 
               items.Add(item);
            }

            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
            
        }


    }

    public void Remove (Item item)
    {

        item.itemAmount = 1;
        items.Remove(item);


        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public static void ItemPickedUp(Item item)
    {
        if(OnItemAddedCallback != null)
        {
           OnItemAddedCallback(item);
        }
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<Item, bool> pair in data.itemsCollected)
        {
            if (pair.Value)
            {
                Debug.Log("item saved");
            }
        }
        this.naturePoints = data.naturePoints; 

    }

    public void SaveData(GameData data)
    {
        foreach (Item item in items)
        {
            data.itemsCollected.Add(item, true);
        }
        data.naturePoints = this.naturePoints;


    }

    public void Test()
    {

        foreach (Item item in items)
        {
            Debug.Log(item.id);
        }

    }
}
