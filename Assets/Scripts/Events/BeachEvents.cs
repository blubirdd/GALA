using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachEvents : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject strandedDugong;
    public GameObject dugongHelper1;
    public GameObject dugongHelper2;
    public GameObject questParent;

    //[Header("Triggers")]
    [Header("First Set Reference")]
    public GameObject firstCharacterSet;

    [Header("Quest 2")]

    public GameObject secondStrandedDuong;
    public GameObject secondCharacterSet;

    void Start()
    {
        secondCharacterSet.SetActive(false);
        GameEvents.instance.onQuestCompleted += BeachQuestCompleteCheck;

    }

    public void BeachQuestCompleteCheck(string questName)
    {
        //QuestTalkStrandedDugongHelpers
        //if(questName == "Find help to save the Dugong")
        //{
        //    dugongHelper1.GetComponent<Animator>().SetBool("Idle", true);
        //    dugongHelper2.GetComponent<Animator>().SetBool("Idle", true);
        //}

        //QuestPushDugong
        if(questName == "Push the stranded Dugong")
        {
            strandedDugong.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            strandedDugong.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            StartCoroutine(PushDugong());
            IEnumerator PushDugong()
            {
                float distanceMoved = 0f;
                Vector3 startPosition = questParent.transform.position;
                Vector3 endPosition = questParent.transform.position + questParent.transform.forward * 13f - Vector3.up * 1f;

                while (distanceMoved < 13f)
                {
                    float distanceThisFrame = 3f * Time.deltaTime;
                    questParent.transform.position = Vector3.MoveTowards(questParent.transform.position, endPosition, distanceThisFrame);
                    distanceMoved += distanceThisFrame;
                    yield return null;
                }

                yield return new WaitForSeconds(2f);
                dugongHelper1.GetComponent<Animator>().SetBool("Idle", true);
                dugongHelper2.GetComponent<Animator>().SetBool("Idle", true);

                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(2f);
                    dugongHelper1.GetComponent<Animator>().SetTrigger("Celebrate");
                    dugongHelper2.GetComponent<Animator>().SetTrigger("Celebrate");

                }

                //activate infos
                firstCharacterSet.SetActive(false);

                secondCharacterSet.SetActive(true);
                ParticleManager.instance.SpawnPuffParticle(secondCharacterSet.transform.position);

            }

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
