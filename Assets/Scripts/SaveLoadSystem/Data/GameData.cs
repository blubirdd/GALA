using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public Vector3 playerPosition;
    public int naturePoints;
    public SerializableDictionary<Item, bool> itemsCollected;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.naturePoints = 0;

        //to change
        playerPosition = Vector3.zero;

        itemsCollected = new SerializableDictionary<Item, bool>();
    }
}
