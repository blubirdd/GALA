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
    private QuestNew quest { get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            subtleDialogue.TriggerDialogue();

            if(objectToEnable != null)
            {
                objectToEnable.SetActive(true);
            }

            if(targetToFocus != null)
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
                    Destroy(gameObject);
                }
            }

            if(questID != "" && targetToFocus == null)
            {
                quest = (QuestNew)questManager.AddComponent(System.Type.GetType(questID));
            }
            
        }
    }

}
