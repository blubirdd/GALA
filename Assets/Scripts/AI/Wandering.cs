using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{

    public float movSpeed;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotL = false;
    private bool isRotR = false;
    private bool isWalking = false;

    Rigidbody rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isWandering == false)
        {
            animator.SetBool("isWalking", false);
            StartCoroutine(Wander());
        }
        if (isRotR == true)
        {
            animator.SetBool("isWalking", false);
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotL == true)
        {
            animator.SetBool("isWalking", false);
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            animator.SetBool("isWalking", true);
            rb.transform.position += transform.forward * movSpeed *Time.deltaTime;
        }
    }
    IEnumerator Wander()
    {
        int rottime = Random.Range(1, 3);
        int rotatelorR = Random.Range(1, 2);
        int walkwait = Random.Range(1, 5);
        int walktime = Random.Range(1, 5);


        isWandering = true;

        yield return new WaitForSeconds(walkwait);
        isWalking = true;
        yield return new WaitForSeconds(walktime);
        isWalking = false;
        if (rotatelorR == 1)
        {
            isRotR = true;
            yield return new WaitForSeconds(rottime);
            isRotR = false;
        }
        if (rotatelorR == 2)
        {
            isRotL = true;
            yield return new WaitForSeconds(rottime);
            isRotL = false;
        }
        isWandering = false;
    }
}

