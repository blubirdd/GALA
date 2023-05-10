using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    private Animator animator;

    public string action;
    public float randomDelaySec;
    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(RandomDelay());

    }

    IEnumerator RandomDelay()
    {
        yield return new WaitForSeconds(randomDelaySec);
        animator.SetBool(action, true);
    }

}
