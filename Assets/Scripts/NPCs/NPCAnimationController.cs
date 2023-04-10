using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Animation")]
    public bool isPanicking;


    Animator animator;
    //IdleNPC idleNpc;
    void Start()
    {
        //idleNpc = GetComponent<IdleNPC>();
        animator = GetComponent<Animator>();


        if (isPanicking)
        {
            animator.SetBool("Panicking", true);
        }
    }

    public void StopPanicking()
    {
        animator.SetBool("Panicking", false);
    }

}
