using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour, ICharacter
{
    public string npcName { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        npcName = "Wildlife Specialist";
    }

    public void Test()
    {
        TalkEvents.CharacterApproach(this);
    }
}
