using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSave : MonoBehaviour, IDataPersistence
{
    private bool isPickedUp;
    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {
        data.hasBackpack = isPickedUp;
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
        isPickedUp = true;
        Task.instance.backpackModel.SetActive(true);
    }
}
