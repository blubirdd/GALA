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
            queueChecker = StartCoroutine(CheckQueue(false));

    }

    public void AddToQueuePickedUp(Item item)
    {
        popupQueue.Enqueue(item);
        if (queueChecker == null)
            queueChecker = StartCoroutine(CheckQueue(true));
    }
    private void ShowPopup(Item item, bool isItemPickUP)
    { //parameter the same type as queue
        window.SetActive(true);

        if (isItemPickUP)
        {
            popupText.text = "Picked up " + item.name;
        }

        else
        {
            if (item.isAnimalFood)
            {
                popupText.text = "Sucessfully fed " + item.name;
            }

            else if (item.isCustomQuestItem)
            {
                popupText.text = "Successfully used " + item.name;
            }

            else if (item.isCustomPopup)
            {
                if (item.isCoin)
                {
                    popupText.text = item.itemDescription + ": " + "<color=#F7DC6F> " + item.value +"</color>";
                }
                else
                {
                    popupText.text = item.itemDescription;

                }
            }
        }

        icon.sprite = item.icon;
        popupAnimator.Play("PopupAnimation");
    }

    private IEnumerator CheckQueue(bool isItemPickup)
    {
        do
        {
            ShowPopup(popupQueue.Dequeue(), isItemPickup);
            do
            {
                yield return null;
            } while (!popupAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));

        } while (popupQueue.Count > 0);
        window.SetActive(false);
        queueChecker = null;
    }

}