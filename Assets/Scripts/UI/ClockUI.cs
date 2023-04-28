using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ClockUI : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform clockHandTransform;

    [SerializeField] private Image clockFill;
    [SerializeField] private TextMeshProUGUI clockText;

    private int duration;
    private int remainingDuration;


    void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
    }

    private void Start()
    {

    }

    public void UpdateClock(int second, QuestNew quest)
    {
        duration = second;
        remainingDuration = second;
        StartCoroutine(UpdateTimer(quest));

        StartCoroutine(WaitUntilQuestCompleted(quest));
    }
    IEnumerator WaitUntilQuestCompleted(QuestNew quest)
    {
        yield return new WaitUntil(() => quest.questCompleted);
        ClockManager.instance.TimerEnd(quest);
    }
    private IEnumerator UpdateTimer(QuestNew quest)
    {
        while(remainingDuration > 0)
        {
            clockText.text = remainingDuration.ToString() + "s";
            clockFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            clockHandTransform.eulerAngles = new Vector3(0, 0, -(duration - remainingDuration) * 360f / duration);

            remainingDuration--;
            
            yield return new WaitForSeconds(1f);
        }

        //OnEnd();
        Debug.Log(quest.questID + "QUEST ID WORKING?");
        ClockManager.instance.TimerEnd(quest);
    }


    public void ResetClock()
    {
        remainingDuration = duration;
    }

    public void UpdateClockIndependent(int second)
    {
        duration = second;
        remainingDuration = second;
        StartCoroutine(UpdateTimerIndependent());
    }

    private IEnumerator UpdateTimerIndependent()
    {
        while (remainingDuration > 0)
        {
            clockText.text = remainingDuration.ToString() + "s";
            clockFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            clockHandTransform.eulerAngles = new Vector3(0, 0, -(duration - remainingDuration) * 360f / duration);

            remainingDuration--;

            yield return new WaitForSeconds(1f);
        }

        ClockManager.instance.IndependentClockEnd();

    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    //private void OnEnd()
    //{
    //    Debug.Log("TIMER END");
    //    this.gameObject.SetActive(false);
    //}

}
