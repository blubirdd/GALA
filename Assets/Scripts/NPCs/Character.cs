using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public string npcName { get; set; }
    [SerializeField] private string characterName;
    // Start is called before the first frame update
    void Start()
    {
        npcName = characterName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
