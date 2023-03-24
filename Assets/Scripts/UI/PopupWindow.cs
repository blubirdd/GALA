using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupWindow : MonoBehaviour
{

    #region Singleton

    public static PopupWindow instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PopupWindow found");
            return;
        }

        instance = this;
    }
    #endregion

    public TextMeshProUGUI popupText;
    public Image icon;

    public GameObject window;
    private Animator popupAnimator;

    private Queue<Item> popupQueue; //make it different type for more detailed popups, you can add different types, titles, descriptions etc
    private Coroutine queueChecker;

    private void Start()
    {
        //window = transform.GetChild(0).gameObject;
        popupAnimator = window.GetComponent<Animator>();
        window.SetActive(false);
        popupQueue = new Queue<Item>();
    }

    public void AddToQueue(Item item)
    {//parameter the same type as queue
        popupQueue.Enqueue(item);
        if (queueChecker == null)
            queueChecker = StartCoroutine(CheckQueue());
    }

    private void ShowPopup(Item item)
    { //parameter the same type as queue
        window.SetActive(true);
        popupText.text = "Picked up " + item.name;
        icon.sprite = item.icon;
        popupAnimator.Play("PopupAnimation");
    }

    private IEnumerator CheckQueue()
    {
        do
        {
            ShowPopup(popupQueue.Dequeue());
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));

        } while (popupQueue.Count > 0);
        window.SetActive(false);
        queueChecker = null;
    }

}