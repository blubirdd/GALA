using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int naturePoints = 0;

    public Quest quest;

    public void Test()
    {
        quest.goal.ItemGathered();
        if (quest.goal.IsReached())
        {
            quest.Complete();
        }
    }
}
