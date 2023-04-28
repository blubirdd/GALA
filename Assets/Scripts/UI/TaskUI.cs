using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;


public class TaskUI : MonoBehaviour
{
    Task task;


    public GameObject taskUI;
    public GameObject taskPanelContainer;

    [SerializeField] private GameObject taskPrefab;

    [Header("Tween Animations")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;


    //[SerializeField] private GameObject goalListPrefab;

    private void Start()
    {
       // GameEvents.onQuestAccepted += InstantiateUI;

        GameEvents.instance.onQuestCompleted += RemoveTask;

        //GameObject itemHolder = transform.GetChild(0).gameObject;

        //if(length > 0)
        //{
        //    Destroy(itemHolder);
        //}
        //UpdateUI();

    }

    public void UpdateUI()
    {
        task = Task.instance;
        if (task.tasks.Count > 0)
        {
            GameObject g;
            for (int i = 0; i < task.tasks.Count; i++)
            {
                g = Instantiate(taskPrefab, transform);

                g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = task.tasks[i].questTitle;
                //Transform goalsContainer = g.transform.FindDeepChild("GoalsContent");
                Transform goalsContainer = g.transform.Find("GoalsContent");

                TextMeshProUGUI goalText = goalsContainer.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI n;

                for (int j = 0; j < task.tasks[i].requiredAmount.Length; j++)
                {
                    n = Instantiate(goalText, goalsContainer);
                    n.GetComponent<TextMeshProUGUI>().text = task.tasks[i].goalDescription[j] + ": " + task.tasks[i].progress[j] + "/" + task.tasks[i].requiredAmount[j];

                }
                Destroy(goalText.gameObject);

            }
            //fixing overlapping
           // LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }

    }

    //public void InstantiateUI(string title, List<Goal> list)
    //{
       
    //}

    public void RemoveTask(string questName)
    {
        if(this.transform.childCount != 0)
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                
                if(questName == this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text)
                {
                    Destroy(this.transform.GetChild(i).gameObject);
                }

            }
        }
    }

    public void OpenTasks()
    {
        taskUI.SetActive(true);
        UIManager.instance.DisableButtonsUIPACK();

        //animate 
        taskPanelContainer.transform.localPosition = new Vector3(0, -500, 0);
        taskPanelContainer.GetComponent<CanvasGroup>().alpha = 0;

        taskPanelContainer.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
        taskPanelContainer.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        UpdateUI();
        SoundManager.instance.PlaySoundFromClips(6);
    }

    public void CloseTasks()
    {
        // Animate position to initial value

        //Vector3 initialPos = rectTransform.anchoredPosition;
        //rectTransform.DOAnchorPosY(initialPos.y - 200, 0.5f, false).SetEase(Ease.InBack).OnComplete(() =>
        //{

        //});



        taskPanelContainer.transform.DOLocalMoveY(-500, 0.5f).SetEase(Ease.InBack);
        taskPanelContainer.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => taskUI.SetActive(false));

        // Destroy children and disable task UI
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        //taskUI.SetActive(false);

        UIManager.instance.EnableButtonsUIPACK();
        SoundManager.instance.PlaySoundFromClips(6);
    }



}
