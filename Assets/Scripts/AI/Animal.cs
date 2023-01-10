using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    AnimalAI animalAI;


    [Header("States")]
    public bool isPassive;
    public bool isAggro;
    public bool isHunting;

    [Header("Tranforms")]
    [SerializeField] private bool showSphere = true;
    public Transform preyTransform;
    public Transform predatorTransform;

    [Header("Targets")]
    
    public LayerMask targetMask;
    [SerializeField] private float _viewRadius;
    [SerializeField] private int _targetsFound;

    [Header("NavMesh")]
    //private NavMeshAgent _agent;
    private readonly Collider[] _colliders = new Collider[3];
    //[Header("Predator")]

    Animator animator;


    private void Awake()
    {
        animalAI = GetComponent<AnimalAI>();

        StartCoroutine("FindTargetWithDelay", 0.5f);

        animator = GetComponent<Animator>();

       // _agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //  isHunting = false;
        // Wander();

    }

    
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTarget();
        }
    }
    void FindTarget()
    {
        //if bug/ clear all _colliders first

        _targetsFound = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, targetMask);

        if(_targetsFound > 0)
        {
            // Set the initial closest distance to the maximum float value
            float closestDistance = float.MaxValue;
            Transform closestTarget = null;

            
            //find the closest target
            for (int i=0; i<_targetsFound; i++)
            {
                    float distance = Vector3.Distance(transform.position, _colliders[i].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = _colliders[i].transform;
                    }
            }

            if (closestTarget.tag == "Prey")
            {
                preyTransform = closestTarget;

                //if animalai pathfinding is attached
                if (animalAI != null)
                {   //move to target
                    animator.SetBool("isWalking", true);
                    animalAI.TargetLocation(preyTransform);
                }
            }

            else
            {
                predatorTransform = closestTarget;

                //run from target
                Vector3 dirToPredator = transform.position - predatorTransform.position;
                Vector3 newPosition = transform.position + dirToPredator;

                //rotate on direction
                Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10 * Time.deltaTime);
               // _agent.SetDestination(newPosition);

            }

  
        }

        else
        {
            if (animalAI != null)
            animalAI.TargetLocation(null);

            preyTransform = null;
            predatorTransform = null;

            animator.SetBool("isWalking", false);
        }

    }

    public void Wander()
    {
        Vector3 randomPoint = Random.insideUnitSphere * _viewRadius;
        randomPoint -= transform.position;
        Debug.Log(randomPoint);
    }



    private void OnDrawGizmos()
    {
        if (showSphere)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);
        }
    }

    //using colliders
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Prey")
    //    {
    //        preyTransform = collision.gameObject.transform;
    //        isHunting = true;

    //        animalAI.TargetLocation(preyTransform);
    //        Debug.Log(collision.gameObject.name + " has entered the view");
    //    }
    //}

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Prey")
    //    {
    //        isHunting = false;
    //        animalAI.TargetLocation(null);
    //        Debug.Log(collision.gameObject.name + " has exited the view");
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(this.transform.position,10);
    //}



}
