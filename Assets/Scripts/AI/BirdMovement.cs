using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Transform[] locations;   // Array of target locations
    public float speed = 5.0f;      // Speed of bird
    public float minWaitTime = 1.0f;// Minimum wait time before moving to next location
    public float maxWaitTime = 3.0f;// Maximum wait time before moving to next location

    private Animator animator;      // Reference to the bird's Animator component
    private int currentTargetIndex; // Index of the current target location
    private bool isFlying;          // Flag indicating whether the bird is currently flying or waiting
    private float waitTimeRemaining;// Amount of time remaining for the current wait period

    void Start()
    {
        animator = GetComponent<Animator>();
        currentTargetIndex = 0;
        isFlying = false;
        waitTimeRemaining = 0.0f;
    }

    void Update()
    {
        if (isFlying)
        {
            // Move towards the current target location
            transform.position = Vector3.MoveTowards(transform.position, locations[currentTargetIndex].position, speed * Time.deltaTime);

            // Rotate the bird to face the target location
            Vector3 direction = locations[currentTargetIndex].position - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }

            // Check if the bird has reached the current target location
            if (transform.position == locations[currentTargetIndex].position)
            {
                // Stop flying and start waiting
                isFlying = false;
                animator.SetBool("Fly", false);
                waitTimeRemaining = Random.Range(minWaitTime, maxWaitTime);
            }
        }
        else
        {
            // Wait for the specified amount of time
            waitTimeRemaining -= Time.deltaTime;
            if (waitTimeRemaining <= 0.0f)
            {
                // Move to the next target location
                currentTargetIndex = (currentTargetIndex + 1) % locations.Length;
                isFlying = true;
                animator.SetBool("Fly", true);
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = locations[0].position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in locations)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

}
