using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public string[] tagsToCheck;

    public float speed, returnSpeed;
    public float range, stopRange;


    [HideInInspector]
    public Transform caster, collidedWith;
    private LineRenderer line;
    private bool hasCollided;


    private void Start()
    {
        line = transform.Find("Line").GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);
            //Check if we have impacted
            if (hasCollided)
            {
                transform.LookAt(caster);
                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist < stopRange)
                {
                    //hook is attached back
                    ThirdPersonController.instance.FishReel();
                    ParticleManager.instance.SpawnPuffParticle(caster.position);
                    Destroy(gameObject);
                }
            }
            else
            {
                var dist = Vector3.Distance(transform.position, caster.position);
                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (collidedWith) { collidedWith.transform.position = transform.position; }
        }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.transform);

            ItemOnly item;
            if (other.TryGetComponent(out item))
            {
                Debug.Log("Collected " + item.item);

                Destroy(other);
            }
        }


    }

    void Collision(Transform col)
    {
        if(col == null)
        {
            speed = speed * 5;
        }

        else
        {
           speed = returnSpeed;
        }
        
        //Stop movement
        hasCollided = true;
        if (col)
        {
            transform.position = col.position;
            collidedWith = col;
            ParticleManager.instance.SpawnWaterSplashParticle(transform.position);
        }
    }
}
