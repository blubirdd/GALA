using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesUnlockManager : MonoBehaviour
{

    #region Singleton

    public static GamesUnlockManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GamesUnlockManager found");
            return;
        }

        instance = this;
    }
    #endregion

    [Header("Level 1 Button")]
    public GameObject level1Lock;
    public GameObject level1Icon;

    [Header("Level 2 Button")]
    public GameObject level2Lock;
    public GameObject level2Icon;

    [Header("Level 3 Button")]
    public GameObject level3Lock;
    public GameObject level3Icon;

    [Header("Level 4 Button")]
    public GameObject level4Lock;
    public GameObject level4Icon;

    [Header("Level 5 Button")]
    public GameObject level5Lock;
    public GameObject level5Icon;

    [Header("Level 6 Button")]
    public GameObject level6Lock;
    public GameObject level6Icon;

    // Start is called before the first frame update
    void Start()
    {
        //if stage 1 done
        if (Task.instance.tasksCompeleted.Contains("QuestTakeVillageQuiz"))
        {
            UnlockLevel1();
        }

        if (Task.instance.tasksCompeleted.Contains("QuestTakeGrasslandQuiz"))
        {
            UnlockLevel2();
        }

        if (Task.instance.tasksCompeleted.Contains("QuestTakeRiverQuiz"))
        {
            UnlockLevel3();
        }

        if (Task.instance.tasksCompeleted.Contains("QuestTakeRiverQuiz"))
        {
            UnlockLevel4();
        }

        if (Task.instance.tasksCompeleted.Contains("QuestTakeSwampQuiz"))
        {
            UnlockLevel5();
        }

        if (Task.instance.tasksCompeleted.Contains("QuestTakeGalaQuiz"))
        {
            UnlockLevel6();
        }
    }

    public void UnlockAll()
    {
        UnlockLevel1();
        UnlockLevel2();
        UnlockLevel3();
        UnlockLevel4();
        UnlockLevel5();
        UnlockLevel6();
    }
    public void UnlockLevel1()
    {
        level1Icon.SetActive(true);
        level1Lock.SetActive(false);
    }

    public void UnlockLevel2()
    {
        level2Icon.SetActive(true);
        level2Lock.SetActive(false);
    }

    public void UnlockLevel3()
    {
        level3Icon.SetActive(true);
        level3Lock.SetActive(false);
    }

    public void UnlockLevel4()
    {
        level4Icon.SetActive(true);
        level5Lock.SetActive(false);
    }

    public void UnlockLevel5()
    {
        level5Icon.SetActive(true);
        level5Lock.SetActive(false);
    }
    public void UnlockLevel6()
    {
        level6Icon.SetActive(true);
        level6Lock.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
