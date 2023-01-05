using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{

    const float minPathUpdateTime = .2f;
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
    }


    public void Update()
    {

    }

    public void TargetLocation(Transform location)
    {
        target = location;

        if (target != null)
        {
            StartCoroutine(UpdatePath());
        }

        else
        {
            StopCoroutine(UpdatePath());
        }
    }


    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful && target!=null)
        {
            path = new AstarPath(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {

        if (target != null)
        {
            if (Time.timeSinceLevelLoad < .3f)
            {
                yield return new WaitForSeconds(.3f);
            }

            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);
                if (target != null)
                {
                    if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                    {
                        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                        targetPosOld = target.position;

                    }
                }

                else
                {
                    yield break;
                }

            }

        }

        else
        {
            Debug.Log("No target found");
        }
    }

    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);
        float speedPercent = 1;


        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {

                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }

                if(target == null)
                {
                    //wait for 1 second before stopping the hunt
                    // yield return new WaitForSeconds(1);

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
                        followingPath = false;
                    }
                }


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

