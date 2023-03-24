using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestTaskUI : MonoBehaviour
{

    #region Singleton

    public static QuestTaskUI instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of QuestTaskUI  found");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private GameObject questTextArea;


    [SerializeField] private GameObject goalPrefab;



    Task task;
    // Start is called before the first frame update
    void Start()
    {
        task = Task.instance;

        UpdateQuestUI();

    }

    public void UpdateQuestUI()
    {
        StartCoroutine(WaitForFewSeconds());
        IEnumerator WaitForFewSeconds()
        {
            yield return new WaitForSeconds(0.1f);

            if (task.tasks.Count > 0)
            {

                GameObject g;
                questTitleText.text = task.tasks[0].questTitle;

                //destroy first before adding again
                for (int i = 1; i < questTextArea.transform.childCount; i++)
                {
                    Transform childTransform = questTextArea.transform.GetChild(i);

                    //destroy the child object.
                    Destroy(childTransform.gameObject);
                }

                for (int j = 0; j < task.tasks[0].goalDescription.Length; j++)
                {
                    g = Instantiate(goalPrefab, questTextArea.transform);
                    TextMeshProUGUI goalText = g.GetComponent<TextMeshProUGUI>();
                    goalText.text = task.tasks[0].goalDescription[j] + ": " + task.tasks[0].progress[j] + "/" + task.tasks[0].requiredAmount[j];

                    GameObject goalCheckbox = g.transform.GetChild(1).gameObject;

                    if (task.tasks[0].progress[j] >= task.tasks[0].requiredAmount[j])
                    {
                        goalCheckbox.SetActive(true);
                    }
                }
            }
            else
            {
                questTitleText.text = "Explore the area";

                for (int i = 1; i < questTextArea.transform.childCount; i++)
                {
                    Transform childTransform = questTextArea.transform.GetChild(i);

                    //destroy the child object.
                    Destroy(childTransform.gameObject);
                }

            }
        }
        
    }

}
