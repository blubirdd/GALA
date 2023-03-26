using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject strandedDugong;
    public GameObject dugongHelper1;
    public GameObject dugongHelper2;
    void Start()
    {
        GameEvents.instance.onQuestCompleted += BeachQuestCompleteCheck;
    }

    public void BeachQuestCompleteCheck(string questName)
    {
        if(questName == "Save the stranded Dugong")
        {
            dugongHelper1.GetComponent<Animator>().SetBool("Idle", true);
            dugongHelper2.GetComponent<Animator>().SetBool("Idle", true);

            strandedDugong.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            strandedDugong.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }


    }

    private void OnDisable()
    {
        //unsubscribe for optimization
        GameEvents.instance.onQuestCompleted -= BeachQuestCompleteCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
