using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RainforestEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rainforestFairy;
    public GameObject threatsCutscene;

    [Header("Hunters")]
    public GameObject hunters;
    public GameObject endingCutsccene;

    public GameObject galaGuard;

    public GameObject endingCanvas;
    public GameObject endingTimeline;
    public TextMeshProUGUI scoreText;
    public GameObject quizCanvas;
    private void Start()
    {
        GameEvents.instance.onQuestAcceptedForSave += RainforestQuestAcceptCheck;
        GameEvents.instance.onQuestCompleted += RainforestQuestCompleteCheck;
    }

    
    public void RainforestQuestAcceptCheck(string questName)
    {
        if(questName == "Find a way up the mountain")
        {
            StartCoroutine(WaitForPrecedence());
            IEnumerator WaitForPrecedence()
            {
                yield return new WaitForSeconds(0.1f);
                TimeController.instance.SetTimeOfDay(10);
            }

        }

        //QuestPhotographThreats
        if (questName == "Photograph and gather evidence")
        {
            threatsCutscene.SetActive(true);
            StartCoroutine(WaitForThreatsCutcsene());
            IEnumerator WaitForThreatsCutcsene()
            {
                UIManager.instance.DisablePlayerMovement();
                yield return new WaitForSeconds(14f);
                UIManager.instance.EnablePlayerMovement();
            }
        }
    }

    public void RainforestQuestCompleteCheck(string questName)
    {
        if(questName == "Find a way up the mountain")
        {
            rainforestFairy.SetActive(false);
        }

        if(questName == "Free the caged animals")
        {
            TimeController.instance.SetTimeOfDayWithoutCutScene(10);
            endingCutsccene.SetActive(true);
            hunters.SetActive(false);

            galaGuard.SetActive(true);

        }

        if(questName == "Take and pass the last quiz")
        {
            endingTimeline.SetActive(true);
            UIManager.instance.DisablePlayerMovement();
            quizCanvas.SetActive(false);
            scoreText.text = "Total score for " + Player.playerName+ ": "+Inventory.instance.naturePoints.ToString();
            StartCoroutine(WaitForTimeline());
            IEnumerator WaitForTimeline()
            {
                yield return new WaitForSeconds(10f);
                endingCanvas.SetActive(true);
            }

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
