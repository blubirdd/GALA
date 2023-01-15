using System.Collections;
using UnityEngine;
using UnityEngine.AI;



public class Animal : MonoBehaviour
{
    AnimalAI animalAI;


    [Header("States")]
    public bool isPassive;
    public bool isAggro;
    public bool isHunting = false;

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

    [Header("ROAMING / WANDER")]
    [SerializeField]
    private NavMeshAgent _animal;
    [SerializeField]
    private LayerMask floorMask = 0;
    private AIStates curState = AIStates.Idle;

    [Header("Running away")]
//

    //a* for wandering
    // public GameObject objectToInstantiate;
    // Transform previousObject;

    //navmesh wander
    private float waitTimer = 0.0f;
    enum AIStates
    {
        Idle,
        Wandering,
        Hunting,
        Running
    }
    //

    Animator animator;


    private void Awake()
    {
        animalAI = GetComponent<AnimalAI>();

        StartCoroutine(FindTargetWithDelay(0f));
        StartCoroutine(RoamAround());

        // animator = GetComponent<Animator>();

        _animal = GetComponent<NavMeshAgent>();



        //StartCoroutine(Testing());

    }

    private void Update()
    {


    }

    //IEnumerator Testing()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5);
    //        Vector3 randomPoint = RandomNavSphere(transform.position, _viewRadius, floorMask);
    //        NavMeshHit hit;
    //        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
    //        {
    //            //destroy the previous object if it exists
    //            //if (previousObject != null)
    //            //{
    //            //    Destroy(previousObject.gameObject);
    //            //}

    //            //instantiate new object and store a reference to it
    //            previousObject = Instantiate(objectToInstantiate, hit.position, Quaternion.identity).transform;
    //            animalAI.TargetLocation(previousObject);
    //        }
    //    }
    //}

    public void Roam()
    {
        switch (curState)
        {
            case AIStates.Idle:
                DoIdle();
                break;
            case AIStates.Wandering:
                DoWander();
                break;
            case AIStates.Hunting:
                break;

            default:
                Debug.LogError("Should not be here, away with you! D:");
                break;
        }
    }

    IEnumerator RoamAround()
    {
        while (true)
        {
           
            if (preyTransform == null)
            {
                Roam();
            }

            else
            {
               _animal.ResetPath();
            }

            yield return null;
        }
    }



    private void DoIdle()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }
        Debug.Log(this + " is Wandering");
        _animal.SetDestination(RandomNavSphere(transform.position, _viewRadius, floorMask));
        curState = AIStates.Wandering;

    }

    private void DoWander()
    {
        if (_animal.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }
        waitTimer = Random.Range(5.0f, 10.0f);
        Debug.Log("Waiting for " + waitTimer);

        curState = AIStates.Idle;


    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        Debug.Log(navHit.position);
        return navHit.position;
    }



    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            if(isHunting == false)
            {
                yield return new WaitForSeconds(delay);
                FindTarget();
            }


            yield return null;
        }
    }
    void FindTarget()
    {
        //if bug/ clear all _colliders first
        
        _targetsFound = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, targetMask);

        if(_targetsFound <= 0)
        {
            if (animalAI != null)
            {
                animalAI.TargetLocation(null);
            }

            preyTransform = null;
            predatorTransform = null;

            //set the speed back to roaming speed
            _animal.speed = 3.5f;

            // animator.SetBool("isWalking", false);
            return;
        }

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
                Debug.Log(this + " is Hunting");
                //if animalai pathfinding is attached
                if (animalAI != null)
                {   //move to target
                 //   animator.SetBool("isWalking", true);
                    animalAI.TargetLocation(preyTransform);
                }
            }

           //run away?
            if(closestTarget.tag == "Player")
            {
                Debug.Log(this + " is Running away");
                predatorTransform = closestTarget;
                
                //run from target
                Vector3 dirToPredator = transform.position - predatorTransform.position;
                Vector3 newPosition = transform.position + dirToPredator;

                //rotate on direction
                Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.deltaTime);

                _animal.speed = 10f;
                _animal.SetDestination(newPosition);

            }

            else
            {

            }

  
        }

    }

    //public void Wander()
    //{

    //    Vector3 randomPoint = Random.insideUnitSphere * _viewRadius;
    //    NavMeshHit hit;
    //    if (NavMesh.SamplePosition(randomPoint, out hit, _viewRadius, NavMesh.AllAreas))
    //    {
    //        randomPoint = hit.position;
    //    }
    //    randomPoint -= transform.position;
    //    Debug.Log(randomPoint);
    //}



    private void OnDrawGizmos()
    {
        if (showSphere)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);

            NavMeshAgent animal = GetComponent<NavMeshAgent>();

            // Only draw the path if the animal is active and has a path
            if (animal != null && animal.path != null)
            {
                // Create a new array to store the path's corner points
                Vector3[] pathCorners = new Vector3[animal.path.corners.Length + 2];

                // Copy the animalt's path corners to the new array
                animal.path.corners.CopyTo(pathCorners, 1);

                // Add the agent's current position and target position to the array
                pathCorners[0] = animal.transform.position;
                pathCorners[pathCorners.Length - 1] = animal.pathEndPosition;

                // Draw a line between each point in the array
                for (int i = 0; i < pathCorners.Length - 1; i++)
                {
                    Gizmos.DrawLine(pathCorners[i], pathCorners[i + 1]);
                }
            }
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
