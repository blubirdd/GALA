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


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            rb.transform.position += transform.forward * movSpeed *Time.deltaTime;
        }
    }
    IEnumerator Wander()
    {
        int rottime = Random.Range(1, 3);
        int rotwait = Random.Range(1, 3);
        int rotatelorR = Random.Range(1, 3);
        int walkwait = Random.Range(1, 3);
        int walktime = Random.Range(1, 3);


        isWandering = true;

        yield return new WaitForSeconds(walkwait);
        isWalking = true;
        yield return new WaitForSeconds(walktime);
        isWalking = false;
        yield return new WaitForSeconds(rotwait);
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

