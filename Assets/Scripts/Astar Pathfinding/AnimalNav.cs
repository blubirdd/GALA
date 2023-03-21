using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AnimalNav : MonoBehaviour
{

    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshold = .5f;

    //location of target
    public Transform target;


    public float speed = 2;
    public float turnSpeed = 3;
    public float turnDst = 1;
    public float stoppingDst = 1;

    AstarPath path;

    Animal animal;


    void Start()
    {
        animal = GetComponent<Animal>();
        //StartCoroutine(UpdatePath());

        //StartCoroutine(WanderAround());

    }

    private void OnEnable()
    {
        StartCoroutine(UpdatePath());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



    public void Update()
    {

    }

    public void TargetLocation(Transform location)
    {
        target = location;

    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        //if (pathSuccessful && target!=null)
        if (pathSuccessful)
        {
            path = new AstarPath(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {

        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        // Keep running the enumerator until target is not null
        while (true)
        {
            // Wait for target to initialize
            if (target == null)
            {
                yield return null;
                continue;
            }

            
            // Update the path
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                var t = target ?? transform;

                yield return new WaitForSeconds(minPathUpdateTime);
                if ((t.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                {
                    PathRequestManager.RequestPath(transform.position, t.position, OnPathFound);
                    targetPosOld = t.position;
                }


            }
        }

    }


    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;

        //transform.LookAt(path.lookPoints[0]);

        //smooth rotation
        Vector3 direction = (path.lookPoints[0] - transform.position).normalized;
        Quaternion tr = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * turnSpeed);

        float speedPercent = 1;


        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {

                if (pathIndex == path.finishLineIndex)
                {
                    //seeker finsihed the path

                    followingPath = false;
                    break;
                }

                if(target == null)
                {
                    //wait for 1 second before stopping the hunt
                    //currently not working
                    ///yield return new WaitForSeconds(5);

                    //stop following
                    followingPath = false;
                    break;
                }

                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        //reach destination
                        followingPath = false;
                        Debug.Log("Target reached");
                    }
                }

                //stop emmediately after loosing sight
                //remove checking for null to stop after reaching a waypoint
                if (target != null)
                {
                    //rotate the unit to waypoint
                    Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
                    
                }

            }

            yield return null;
            
              
        }
    }



    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}

