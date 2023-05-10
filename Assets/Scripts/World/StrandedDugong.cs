using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StrandedDugong : MonoBehaviour
{
    Character character;
    public float speed = 50.0f;
    public GameObject sea;
    private Rigidbody rb;

    public Item stick;
    private void Start()
    {
        character = GetComponent<Character>();
        rb = GetComponent<Rigidbody>();

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Water"))
        {
            TriggerQuest();
            Destroy(other.gameObject);

            //trigger particle
            ParticleManager.instance.SpawnWaterSplashParticle(transform.position);
        }

        if (other.gameObject.CompareTag("Water Trigger"))
        {
            ParticleManager.instance.SpawnPuffParticle(transform.position);
            transform.LookAt(sea.transform);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            StartCoroutine(Swim());
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (Task.instance.tasksCompeleted.Contains("QuestTalkStrandedDugongHelpers"))
            {
                Inventory.instance.ItemUsed(stick);

            }
        }
            
    }

    public void TriggerQuest()
    {
        TalkEvents.CharacterApproach(character);
    }

    IEnumerator Swim()
    {
        StartCoroutine(DisableAfterSwim());
        while (true)
        {
            rb.velocity = transform.forward * speed;
            yield return null;
        }
    }


    private IEnumerator DisableAfterSwim()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
