using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Animal : MonoBehaviour, IAnimal
{
    AnimalNav animalNav;

    public string animalName { get; set; }
    public string animalGroup { get; set; }
    public string scientificName { get; set; }

    [Header("Animal SO")]
    public Photograph photo;
    public bool isDiscovered = false;

    [Header("AI States")]
    public bool isPassive;
    public bool isAggro;
    public bool isHunting = false;
    public bool chasePlayer = false;
    //private bool isChasingPlayer;
    //food states
    public bool isHungry;
    public bool isThirsty;
    public bool isEating;


    [Header("Quest States")]
    public Item food;
    public bool runawayFromPlayer = true;
    public bool isInjured;
    public bool isRunningAway = false;
    public bool canMove = true;
    public bool canBePickedUp = false;
    public bool playerisInRange = false;
    public bool depleteHunger = true;


    [Header("State floats")]
    [Range(1, 100)]
    public int hunger;
    [Range(1, 100)]
    public int thirst;

    public string playerFoodTag;

    [SerializeField] private float _speed;

    [SerializeField] private float _runSpeed;

    [Header("Tranforms")]
    [SerializeField] private bool showSphere = true;
    public Transform preyTransform;
    public Transform predatorTransform;
    [Header("BODY PART")]
    public bool canBeCaptured = false;
    public GameObject netPart;
    [SerializeField] private bool isCapturedOnStart = false;

    [Header("Targets")]

    public LayerMask targetMask;
    public LayerMask playerMask;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _playerAwarenessRadius = 8;
    [SerializeField] private int _targetsFound;
    [SerializeField] private int _playerFound;

    [Header("NavMesh")]

    private readonly Collider[] _colliders = new Collider[3];
    private readonly Collider[] _playerColliders = new Collider[1];
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

    public Animator animator;

    private void Awake()
    {
        animalNav = GetComponent<AnimalNav>();
        //statusImage.gameObject.SetActive(false);

        // StartCoroutine(FindTargetWithDelay(0.5f));
        // StartCoroutine(RoamAround(0.5f));

        animator = GetComponentInChildren<Animator>();

        _animal = GetComponent<NavMeshAgent>();

        //StartCoroutine(Testing());

        //states 
        // hunger = 50f;
        // thirst = 100f;
        if (isCapturedOnStart)
        {
            netPart.SetActive(true);
            ChangeUI(AnimalStateUI.Captured);
        }
        
    }
    void Start()
    {
        animalName = photo.name;
        animalGroup = photo.animalGroup;
        scientificName = photo.scientificName;

        if (isInjured)
        {
            ChangeUI(AnimalStateUI.Injured);
        }

    }



    void OnEnable()
    {
        if(canMove)
        {
            StartCoroutine(FindTargetWithDelay(1f));
            StartCoroutine(RoamAround(0.5f));

            StartCoroutine(FindPlayerWithDelay());

            if (depleteHunger)
            {
                StartCoroutine(SimulateHungerAndThirst(5f));
            }
        }

        //states 
        hunger = 50;
        thirst = 100;
    }

    public void ActivateAnimal()
    {
        canMove = true;
        StartCoroutine(FindTargetWithDelay(0.5f));
        StartCoroutine(RoamAround(0.5f));

        StartCoroutine(FindPlayerWithDelay());

        if (depleteHunger)
        {
            StartCoroutine(SimulateHungerAndThirst(10f));
        }

        //states 
        hunger = 50;
        thirst = 100;
    }

    public void DeactivateAnimal()
    {
        StopAllCoroutines();
    }

    public void HealAnimal()
    {
        isInjured = false;
        ChangeUI(AnimalStateUI.Idle);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        //hunger -= Time.deltaTime;
        //thirst -= Time.deltaTime;
        //Debug.Log(this + "Speed is "+ _animal.speed);

        //remove null check on final build
        if(animator != null)
        {
            if (!canMove)
            {
                animator.enabled = false;
                return;
            }

            if (_animal.velocity.magnitude <= 0.1f)
            {
                if (isHunting)
                {
                    if (isEating)
                    {
                        animator.SetFloat("AnimSpeed", 0f);
                        Debug.Log(this + "is eating");
                    }
                    else
                    {
                        animator.SetFloat("AnimSpeed", 0.5f);
                    }

                }

                else
                {
                    if(chasePlayer && animalNav.target != null)
                    {
                        animator.SetFloat("AnimSpeed", 0.5f);
                    }
                    else
                    {
                        animator.SetFloat("AnimSpeed", 0f);
                    }

                }
                
                return;
            }

            else if (isRunningAway)
            {
                animator.SetFloat("AnimSpeed", 1f);
                return;
            }

            else 
            {
                //transition from running to walk
                animator.SetFloat("AnimSpeed", 0.5f, 0.5f, Time.deltaTime);
                return;
            }
        }
    }

    IEnumerator SimulateHungerAndThirst(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            hunger -=5;
            //thirst -= 10;
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


        if (!Book.instance.photosInventory.Contains(photo))
        {
            Debug.Log("NEWLY ISCOVERED ADDED TO DATABASE: " + animalName);
            IndicatorController.instance.EnableBookIndicator();
            IndicatorController.instance.EnableBookRedCircle();

            StartCoroutine(WaitForPhoto());
            IEnumerator WaitForPhoto()
            {
                yield return new WaitForEndOfFrame();
                Inventory.instance.itemDiscovery.NewItemDiscovered(photo.polaroidPhoto, photo.name, "New Animal Discovered. Check your journal for more details", false);
            }
          
        }

        Book.instance.AddAnimalPhoto(photo);

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
                if (canMove)
                {
                    Roam();
                }

            }

            //reset the wander and enter wander state
            else
            {
                _animal.ResetPath();
                Debug.Log("RESETING PATH");
                //below code is optimization
                yield return new WaitUntil(() => animalNav.target == null);

            }

            yield return null;
        }
    }

    public enum AnimalStateUI
    {
        Idle,
        RunningAway,
        Eeating,
        Drinking,
        Hunting,
        Injured,
        Captured

    }

    public void ChangeUI(AnimalStateUI status)
    {
        //parent if statement so that it will always show injury status
        if (isInjured && status != AnimalStateUI.RunningAway)
        {
            if (statusImage.sprite == animalState.injured)
            {
                //return if already set
                return;
            }

            else
            {
                statusImage.sprite = animalState.injured;
            }
        }

        else
        {
            if (canBeCaptured && !canMove)
            {
                if (status == AnimalStateUI.Captured)
                {
                    if (statusImage.sprite == animalState.net && netPart.activeSelf)
                    {
                        return;
                    }

                    else
                    {
                        netPart.SetActive(true);
                        statusImage.sprite = animalState.net;
                    }

                    return;
                }

                else
                {
                    if (!netPart.activeSelf)
                    {
                        return;
                    }

                    netPart.SetActive(false);
                }
            }

            if (status == AnimalStateUI.RunningAway)
            {
                if (statusImage.sprite == animalState.runningAway)
                {
                    return;
                }

                else
                {
                    statusImage.sprite = animalState.runningAway;
                }

                return;
            }

            if (status == AnimalStateUI.Eeating)
            {
                if (statusImage.sprite == animalState.eating)
                {
                    return;
                }

                else
                {
                    statusImage.sprite = animalState.eating;
                }

                return;
            }

            if (status == AnimalStateUI.Idle)
            {
                if (statusImage.sprite == animalState.normal)
                {
                    return;
                }

                else
                {
                    statusImage.sprite = animalState.normal;
                }
            }

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
        //Debug.Log("Transition to idle");
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

    IEnumerator FindPlayerWithDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            FindPlayer();

            yield return null;
        }
    }

    private void FindPlayer()
    {
        _playerFound = Physics.OverlapSphereNonAlloc(transform.position, _playerAwarenessRadius, _playerColliders, playerMask);

        if(_playerFound > 0 && canMove)
        {
            if (_playerColliders[0].CompareTag("Player"))
            {
                if(chasePlayer == false)
                {

                    if (runawayFromPlayer)
                    {
                        statusImage.gameObject.SetActive(true);

                        //statusImage.sprite = animalState.runningAway;

                        //new optimized
                        isRunningAway = true;
                        ChangeUI(AnimalStateUI.RunningAway);

                        predatorTransform = _playerColliders[0].transform;

                        Debug.Log(this + " is Running away");

                        //run from target
                        Vector3 dirToPredator = transform.position - predatorTransform.position;
                        Vector3 newPosition = transform.position + dirToPredator * 1.3f;

                        //rotate on direction
                        Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1 * Time.deltaTime);

                        _animal.speed = _runSpeed;
                        _animal.SetDestination(newPosition);
                        //animator.SetFloat("AnimSpeed", 1f);
                    }


                }

                else
                {
                    animalNav.target = _playerColliders[0].transform;
                    animalNav.speed = _runSpeed;
                    //isChasingPlayer = true;
                    UIManager.instance.emote.SetActive(true);

                }

            }

        }

        else
        {
            //predatorTransform = null;
            //statusImage.gameObject.SetActive(false);
            isRunningAway = false;

            if (_animal.speed == _speed)
            {
                //return;
            }
            else
            {
                _animal.speed = _speed;
            }

            if (isHunting == false)
            {
                if (canMove)
                {
                    ChangeUI(AnimalStateUI.Idle);
                }

                if (animalNav.target == null)
                {
                    return;
                }

                else
                {
                    if (canMove)
                    {
                        ChangeUI(AnimalStateUI.Idle);
                    }

                    UIManager.instance.emote.SetActive(false);
                    //isChasingPlayer = false;
                    animalNav.target = null;
                    animalNav.speed = _speed;
                }

            }

        }
    }


    //find target when hungry/thirsty
    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            //if(isHungry || isThirsty)
            //{
            //    FindTarget();
            //}
            if (isHungry && canMove)
            {
                FindTarget();
            }
            yield return null;
        }
    }
    void FindTarget()
    {
        //if bug/ clear all _colliders first
        //Debug.Log(this + " is Finding target");
        
        _targetsFound = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, targetMask);

        if(_targetsFound <= 0)
        {
            //check if animalnav is with component
            // if (animalNav != null)
            // {
            //     animalNav.TargetLocation(null);
            // }

            // preyTransform = null;
            //animalNav.target = null;
            isHunting = false;
            animalNav.target = null;

            //set the speed back to roaming speed
            _animal.speed = _speed;

            //disable status image
            

            //animation
            // animator.SetBool("isWalking", false);
            return;
        }

        if(_targetsFound > 0)
        {

            for (int i = 0; i < _targetsFound; i++)
            {
                //moved to new

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

                        isHunting = true;
                    }

                    //else if (_colliders[i].CompareTag("Prey") && isHungry)
                    else if (_colliders[i].CompareTag("Prey"))
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

                        //
                        //statusImage.sprite = animalState.eating;
                        ChangeUI(AnimalStateUI.Eeating);

                        isHunting = true;
                        // break;
                    }

                    else if(_colliders[i].CompareTag("HerbivoreFood"))
                    {
                        Debug.Log("FOUND FOOD FROM PLAYER");
                        animalNav.target = _colliders[i].transform;
                        statusImage.gameObject.SetActive(true);
                        statusImage.sprite = animalState.eating;

                        isHunting = true;
                    }

                    else if (_colliders[i].CompareTag("Meat"))
                    {
                        Debug.Log("FOUND FOOD FROM PLAYER");
                        animalNav.target = _colliders[i].transform;
                        //animalNav.speed = _runSpeed;
                        statusImage.gameObject.SetActive(true);
                        statusImage.sprite = animalState.eating;

                        isHunting = true;
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
        if (other.gameObject.CompareTag("Player"))
        {
            //if not aggressive
            if(chasePlayer == false)
            {
                if (food != null)
                {
                    Interactor interactor;
                    playerisInRange = true;
                    interactor = other.GetComponent<Interactor>();

                    if (food == EquipmentManager.instance.currentEquipment[0] && isInjured == false)
                    {
                        interactor._interactionPromptUI.Setup("Feed " + photo.name, food.icon);
                    }
                }
            }


            //if aggressive
            else
            {
                if (canMove)
                {
                    animator.SetTrigger("Attack");
                    UIManager.instance.DisablePlayerMovement();
                    ThirdPersonController.canMove = false;
                    //other.GetComponent<ThirdPersonController>().crouching = true;
                    this.GetComponent<CapsuleCollider>().enabled = false;
                    Vector3 direction = other.transform.position - transform.position;

                    // Ignore the y-axis rotation to only rotate horizontally
                    direction.y = 0;

                    // Rotate this object to face the target object
                    transform.rotation = Quaternion.LookRotation(direction);
                    
                    StartCoroutine(WaitForAttackAnimation());
                    IEnumerator WaitForAttackAnimation()
                    {
                        yield return new WaitForSeconds(1f);
                        Player.instance.EnablePlayerDiedUI();
                        Debug.Log("Caught player");
                        UIManager.instance.EnablePlayerMovement();
                        ThirdPersonController.canMove = true;
                        this.GetComponent<CapsuleCollider>().enabled = true;
                    }

                }

            }

        }

        if (other.gameObject.CompareTag("Meat"))
        {
            //if aggressive
            if (chasePlayer)
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
                    ParticleManager.instance.SpawnPuffParticle(transform.position);

                    animator.SetTrigger("Attack");
                    yield return new WaitForSeconds(1.5f);
                    //other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    hunger = 100;
                    
                    Destroy(other);
                    // yield return new WaitForSeconds(10);
                    // other.gameObject.SetActive(true);
                }
            }
        }

        if (other.gameObject.CompareTag("Prey"))
        {
            //if not aggressive
            if (!chasePlayer)
            {
                //set view radius to normal
                //_viewRadius = 15f;
                //do something

                StartCoroutine(EatDelayFood());
                IEnumerator EatDelayFood()
                {
                    //trigger the quest event
                    Debug.Log(this + "just ate");

                    yield return new WaitForSeconds(1.5f);
                    other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    hunger = 100;

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

                    isEating = true;
                    
                    //trigger the quest event
                    FeedEvents.AnimalFed(this);
                    animator.SetTrigger("Eat");
                    Debug.Log(this + "just ate");

                    yield return new WaitForSeconds(5);
                    //other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;
                    
                    hunger = 100;
                    isEating = false;

                    Destroy(other);
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
                    yield return new WaitForSeconds(7);
                    //other.gameObject.SetActive(false);

                    //preyTransform = null;
                    animalNav.target = null;

                    thirst = 150;

                    //yield return new WaitForSeconds(10);
                    //other.gameObject.SetActive(true);
                }


            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerisInRange = false;
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

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _playerAwarenessRadius);
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
