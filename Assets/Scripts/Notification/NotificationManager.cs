using DG.Tweening;
using System;
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

    private Queue<string> notificationQueue = new Queue<string>();
    private bool isDisplaying = false;

    [Header("Completed Notif UI's")]
    [SerializeField] private GameObject checkMark;
    private void Start()
    {
        GameEvents.instance.onQuestAcceptedNotification += AddQuestNotification;
        GameEvents.instance.onQuestCompleted += AddQuestNotificationCompleted;

    }

    public void AddQuestNotification(string questName)
    {
        notificationQueue.Enqueue(questName);
        if (!isDisplaying)
        {
            DisplayNextNotification(false);
        }

    }

    public void AddQuestNotificationCompleted(string questName)
    {

        notificationQueue.Enqueue(questName);
        if (!isDisplaying)
        {
            DisplayNextNotification(true);
        }

    }

    private void DisplayNextNotification(bool isCompletedNotification)
    {
        if (notificationQueue.Count > 0)
        {
            notificationCanvas.SetActive(true);
            animator.SetBool("IsDisplaying", true);

            string questName = notificationQueue.Dequeue();

            //IF ACCEPT
            if(isCompletedNotification == false)
            {
                checkMark.SetActive(false);
                questNameText.fontStyle = FontStyles.Bold;
                questNameText.SetText(questName);
                pencilWriter.WriteText();
            }
            //IF COMPLETE
            else
            {
                checkMark.SetActive(true);
                var duration = 0.5f;
                var sequence = DOTween.Sequence();
                sequence.Append(checkMark.transform.DOScale(1.2f, duration));
                sequence.Append(checkMark.transform.DOScale(0.8f, duration));
                sequence.Append(checkMark.transform.DOScale(1.0f, duration));

                sequence.Play();
                questNameText.SetText(questName);

                questNameText.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;

            }
            
            

            isDisplaying = true;

            if(isCompletedNotification == true)
            {
                StartCoroutine(WaitForDelay(true));
            }

            else
            {
                StartCoroutine(WaitForDelay(false));
            }
            
        }
        else
        {
            isDisplaying = false;
        }
    }



    IEnumerator WaitForDelay(bool isCompletedNotification)
    {
        yield return new WaitForSeconds(5f);
        animator.SetBool("IsDisplaying", false);

        //turn off canvas if performance issues
        //turn off canvas
        

        if (notificationQueue.Count > 0)
        {
            if(isCompletedNotification == true)
            {
                DisplayNextNotification(true);
            }

            else
            {
                DisplayNextNotification(false);
            }
            
        }
        else
        {
            isDisplaying = false;
        }
    }


}
