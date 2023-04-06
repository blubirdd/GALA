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

    public int duration;
    private int remainingDuration;


    void Awake()
    {
        clockHandTransform = transform.Find("clockHand");
    }

    private void Start()
    {
        UpdateClock(duration);
    }

    public void UpdateClock(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration > 0)
        {
            clockText.text = remainingDuration.ToString() + "s";
            clockFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            clockHandTransform.eulerAngles = new Vector3(0, 0, -(duration - remainingDuration) * 360f / duration);

            remainingDuration--;
            
            yield return new WaitForSeconds(1f);
        }

        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("TIMER END");
        this.gameObject.SetActive(false);
    }

}
