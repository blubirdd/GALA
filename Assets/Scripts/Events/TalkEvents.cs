using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TalkEvents : MonoBehaviour
{
    public delegate void TalkEventHandler(ICharacter npc);

    public static event TalkEventHandler onCharacterApproach;

    public static void CharacterApproach(ICharacter npc)
    {
        if (onCharacterApproach != null)
        {
            onCharacterApproach(npc);
        }

    }
}

