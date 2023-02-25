using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Animal : MonoBehaviour, IAnimal
{
    AnimalNav animalNav;

    public string animalName { get; set; }
    public string animalGroup { get; set; }
    [Header("Animal SO")]
    public Photograph photo;

    [Header("States")]
    public bool isPassive;
    public bool isAggro;
    public bool isHunting = false;
    
    //food states
    public bool isHungry;
    public bool isThirsty;

    [Header("State floats")]
    [Range(1, 100)]
    public float hunger;
    [Range(1, 100)]
    public float thirst;

    public string playerFoodTag;

    [SerializeField] private float _speed;

    [SerializeField] private float _runSpeed;

    [Header("Tranforms")]
    [SerializeField] private bool showSphere = true;
    public Transform preyTransform;
    public Transform predatorTransform;

    [Header("Targets")]
    
    public LayerMask targetMask;
    [SerializeField] private float _viewRadius;
    [SerializeField] private int _targetsFound;

    [Header("NavMesh")]
    
    private readonly Collider[] _colliders = new Collider[3];
    //[Header("Predator")]

    [Header("ROAMING / WANDER")]
    [SerializeField]
    private NavMeshAgent _animal;
    [SerializeField]
    private LayerMask floorMask = 0;
    private AIStates curState = AIStates.Idle;

    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    [Header("Running away")]
    [Header("Sprites")]
    public Image statusImage;
    public AnimalState animalState;
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
        RunningAway
    }
    //

    Animator animator;

    void Start()
    {
        animalName = photo.name;
        animalGroup = photo.animalGroup;
    }

    private void Awake()
    {
        animalNav = GetComponent<AnimalNav>();
        statusImage.gameObject.SetActive(false);

        // StartCoroutine(FindTargetWithDelay(0.5f));
        // StartCoroutine(RoamAround(0.5f));

        // animator = GetComponent<Animator>();

        _animal = GetComponent<NavMeshAgent>();

        //StartCoroutine(Testing());

        //states 
        // hunger = 50f;
        // thirst = 100f;


    }






    void OnEnable()
    {
        StartCoroutine(FindTargetWithDelay(0.5f));
        StartCoroutine(RoamAround(0.5f));

        //states 
        hunger = 50f;
        thirst = 100f;

    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        hunger -= Time.deltaTime;
        thirst -= Time.deltaTime;

        if (hunger <= 0)
        {
          //  _viewRadius = 30f;
            isHungry = true;
            hunger = 0;
        }

        else
        {
            isHungry = false;
        }

        if (thirst <= 0)
        {
           //9 _viewRadius = 30f;
            isThirsty = true;
           thirst = 0;
        }

        else
        {
            isThirsty = false;
        }
    }

    //using astar algo
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

    public void Discovered()
    {
        PictureEvents.AnimalDiscovered(this);
        Book.instance.AddAnimalPhoto(photo);

        Debug.Log("NEWLY ISCOVERED ADDED TO DATABASE: " + animalName);
    }

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
                //method here
                break;
            case AIStates.RunningAway:
                //method here
                break;

            default:
                Debug.LogError("There's an error");
                break;
        }
    }

    IEnumerator RoamAround(float delay)
    {
        while (true)
        {
            //if there is no current target
            // if (preyTransform == null)
            if (animalNav.target == null)
            {
                Roam();
            }

            //reset the wander and enter wander state
            else
            {
               _animal.ResetPath();
            }

            yield return null;
        }
    }



    private void DoIdle()
    {
        //wait for the timer before wanderinga gain
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }
        //Debug.Log(this + " is Wandering");
        _animal.SetDestination(RandomNavSphere(transform.position, _viewRadius, floorMask));
        curState = AIStates.Wandering;

    }

    private void DoWander()
    {
        if (_animal.pathStatus != NavMeshPathStatus.PathComplete)
        {
            return;
        }

        waitTimer = Random.Range(minWaitTime, maxWaitTime);
        //Debug.Log("Waiting for " + waitTimer);

        curState = AIStates.Idle;
    }

    //generate random points within radius
    Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        //Debug.Log(navHit.position);
        return navHit.position;
    }


    //find target when hungry/thirsty
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            FindTarget();

            yield return null;
        }
    }
    void FindTarget()
    {
        //if bug/ clear all _colliders first
        
        _targetsFound = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, targetMask);

        if(_targetsFound <= 0)
        {
            //check if animalnav is with component
            // if (animalNav != null)
            // {
            //     animalNav.TargetLocation(null);
            // }

           // preyTransform = null;
           animalNav.target = null;

           predatorTransform = null;

            //set the speed back to roaming speed
            _animal.speed = _speed;

            //disable status image
            statusImage.gameObject.SetActive(false);

            //animation
            // animator.SetBool("isWalking", false);
            return;
        }

        if(_targetsFound > 0)
        {
            
            for (int i = 0; i < _targetsFound; i++)
            {
                if (_colliders[i].CompareTag("Player"))
                {
                    statusImage.gameObject.SetActive(true);
                    statusImage.sprite = animalState.runningAway;
                    predatorTransform = _colliders[i].transform;

                    Debug.Log(this + " is Running away");

                    //run from target
                    Vector3 dirToPredator = transform.position - predatorTransform.position;
                    Vector3 newPosition = transform.position + dirToPredator * 1.5f;

                    //rotate on direction
                    Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.deltaTime);

                    _animal.speed = _runSpeed;
                    _animal.SetDestination(newPosition);
                }

                else
                {
                    predatorTransform = null;
                    statusImage.gameObject.SetActive(false);
                }

            }

            Transform FindTheClosestTarget()
            {
                Transform closestTarget = null;
                // Set the initial closest distance to the maximum float value
                float closestDistance = float.MaxValue;

                //find the closest target
                for (int i = 0; i < _targetsFound; i++)
                {

                    if (_colliders[i].CompareTag("Water") && isThirsty)
                    {
                        float distance = Vector3.Distance(transform.position, _colliders[i].transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = _colliders[i].transform;
                        }
                        animalNav.target = closestTarget;
                        // preyTransform = closestTarget;
                        // break;
                    }

                    else if (_colliders[i].CompareTag("Prey") && isHungry)
                    {
                        float distance = Vector3.Distance(transform.position, _colliders[i].transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = _colliders[i].transform;
                        }
                        animalNav.target = closestTarget;

                        // preyTransform = _colliders[i].transform;
                        // animalNav.target = _colliders[i].transform;

                        Debug.Log(this +" is hunting");

                        statusImage.gameObject.SetActive(true);
                        statusImage.sprite = animalState.eating;
                        // break;
                    }

                    else if(_colliders[i].CompareTag("HerbivoreFood"))
                    {
                        Debug.Log("FOUND FOOD FROM PLAYER");
                        animalNav.target = _colliders[i].transform;
                        statusImage.gameObject.SetActive(true);
                        statusImage.sprite = animalState.eating;
                    }

                }

                return closestTarget;
            }

            

            //if thirsty look for water
             //if hungry look for food
            if (isThirsty == true || isHungry == true)
            {
               // animalNav.TargetLocation(FindTheClosestTarget());
               FindTheClosestTarget();
            }
            
            //if (isThirsty)
            //{
            //    Debug.Log(this + " is thirsty");

            //    for (int i = 0; i < _targetsFound; i++)
            //    {

            //        if (_colliders[i].CompareTag("Water"))
            //        {
            //            preyTransform = _colliders[i].transform;
            //            if (animalAI != null)
            //            {
            //                animalAI.TargetLocation(preyTransform);
            //            }

            //            break;
            //        }
            //    }

            //}

            //if hungry look for food
            //if (isHungry)
            //{
            //    Debug.Log(this + " is Hungry");

            //    for (int i = 0; i < _targetsFound; i++)
            //    {

            //        if (_colliders[i].CompareTag("Prey"))
            //        {
            //            preyTransform = _colliders[i].transform;
            //            if (animalAI != null)
            //            {
            //                animalAI.TargetLocation(preyTransform);
            //            }

            //            break;
            //        }
            //    }

            //}



            //if (closestTarget.CompareTag("Prey") && isHungry == true)
            //{
            //    preyTransform = closestTarget;
            //    Debug.Log(this + " is Hunting");

            //    //if animalai pathfinding is attached
            //    if (animalAI != null)
            //    {   //move to target
            //        //   animator.SetBool("isWalking", true);
            //        animalAI.TargetLocation(preyTransform);
            //    }
            //}

            ////run away?
            //if (closestTarget.CompareTag("Player"))
            //{

            //    statusImage.sprite = animalState.runningAway;
            //    Debug.Log(this + " is Running away");
            //    predatorTransform = closestTarget;

            //    //run from target
            //    Vector3 dirToPredator = transform.position - predatorTransform.position;
            //    Vector3 newPosition = transform.position + dirToPredator * 1.5f;

            //    //rotate on direction
            //    Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.deltaTime);

            //    _animal.speed = 10f;
            //    _animal.SetDestination(newPosition);

            //}

        }




    }

    //if animal collided with food
    //animal is eating
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prey"))
        {
            if (isHungry)
            {
                //set view radius to normal
                //_viewRadius = 15f;
                //do something

                StartCoroutine(EatDelayFood());
                IEnumerator EatDelayFood() 
                {

                    yield return new WaitForSeconds(5);
                    other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    hunger = 100f;
                    
                    yield return new WaitForSeconds(10);
                    other.gameObject.SetActive(true);
                }
            }
        }

        if (other.gameObject.CompareTag("HerbivoreFood"))
        {

                //set view radius to normal
                //_viewRadius = 15f;
                //do something

                StartCoroutine(EatDelayFood());
                IEnumerator EatDelayFood() 
                {

                    //trigger the quest event
                    FeedEvents.AnimalFed(this);
                    Debug.Log(this + "just ate");

                    yield return new WaitForSeconds(5);
                    other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    hunger = 100f;
                    
                    // yield return new WaitForSeconds(10);
                    // other.gameObject.SetActive(true);
                }
        }

        if (other.gameObject.CompareTag("Water"))
        {
            if (isThirsty)
            {   //set view radius to normal
                //_viewRadius = 15f;

                //do something
                StartCoroutine(EatDelayWater());
                IEnumerator EatDelayWater()
                {
                    yield return new WaitForSeconds(5);
                    other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    thirst = 150f;

                    yield return new WaitForSeconds(10);
                    other.gameObject.SetActive(true);
                }


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
