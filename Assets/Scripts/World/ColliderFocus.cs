using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFocus : MonoBehaviour
{
    public SubtleDialogueTrigger subtleDialogue;
    public Transform targetToFocus;
    public float duration;

    [Header("If Has to Enable Object")]
    public GameObject objectToEnable;

    [Header("Quest")]
    public GameObject questManager;
    public string questID;
    public bool giveQuestDelay = false;
    Character character;
    public bool disablePlayerMovement = false;
    public float movementDisbleDuration = 0;
    public bool turnToNight = false;

    private bool isdone;
    private QuestNew quest { get; set; }

    private void Start()
    {
        character = GetComponent<Character>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!isdone)
            {
                subtleDialogue.TriggerDialogue();

                if (objectToEnable != null)
                {
                    objectToEnable.SetActive(true);
                }

                if (targetToFocus != null)
                {
                    CinemachineManager.instance.lookAtTargetCamera.gameObject.SetActive(true);
                    CinemachineManager.instance.lookAtTargetCamera.Follow = targetToFocus;
                    CinemachineManager.instance.lookAtTargetCamera.LookAt = targetToFocus;

                    StartCoroutine(WaitForSeconds());
                    IEnumerator WaitForSeconds()
                    {
                        yield return new WaitForSeconds(duration);
                        CinemachineManager.instance.lookAtTargetCamera.gameObject.SetActive(false);
                        quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));
                        //Destroy(gameObject);


                    }
                }

                if (disablePlayerMovement)
                {
                    StartCoroutine(DisableMovement());
                    IEnumerator DisableMovement()
                    {
                        UIManager.instance.DisablePlayerMovement();
                        yield return new WaitForSeconds(movementDisbleDuration);
                        UIManager.instance.EnablePlayerMovement();
                    }
                }
                if (turnToNight)
                {
                    StartCoroutine(WaitToturnNight());
                    IEnumerator WaitToturnNight()
                    {
                        yield return new WaitForSeconds(13f);
                        TimeController.instance.SetTimeOfDay(19);
                    }

                }

                if (questID != "" && targetToFocus == null)
                {
                    if (giveQuestDelay)
                    {
                        StartCoroutine(GiveQuestDelay());

                    }
                    else
                    {
                        quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));

                    }

                }


                if (character != null)
                {
                    TalkEvents.CharacterApproach(character);
                }

                if (!giveQuestDelay)
                {
                    Destroy(gameObject);
                }

                isdone = true;
            }

           
        }
    }

    IEnumerator GiveQuestDelay()
    {
        yield return new WaitForSeconds(5f);
        quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));
        Destroy(gameObject);
    }

}
