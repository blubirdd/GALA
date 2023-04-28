using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoItemAdd : MonoBehaviour
{
    // Start is called before the first frame update
    public Item[] itemsToAdd;
    void Start()
    {
        foreach (var item in itemsToAdd)
        {
            Inventory.instance.Add(item, 1, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
