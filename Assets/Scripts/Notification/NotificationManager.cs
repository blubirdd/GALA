using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject notificationCanvas;
    [SerializeField] private GameObject questNotificationPanel;
    [SerializeField] private TextMeshProUGUI questNameText;
    private Vector3 initialPos;

    public PencilWriter pencilWriter;
    private void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += DisplayQuestNotification;
    }


    public void DisplayQuestNotification(string questName)
    {
        notificationCanvas.SetActive(true);
        questNameText.SetText(questName);
        StartCoroutine(WaitForDelay());


        pencilWriter.WriteText();
        animator.SetBool("IsDisplaying", true);

        Debug.Log(questName);
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(6f);
        animator.SetBool("IsDisplaying", false);

    }

    public void TestButton()
    {

    }


}
