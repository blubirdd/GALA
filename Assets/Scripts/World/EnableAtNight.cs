using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAtNight : MonoBehaviour
{
    TimeController timeController;
    public GameObject animals;
    void Start()
    {
        timeController = TimeController.instance;
        StartCoroutine(CheckForTime());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CheckForTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (timeController.timeHour >= 19 || timeController.timeHour < 6)
            {
                if(animals.gameObject.activeSelf)
                {
                    yield return null;
                }
                else
                {
                    animals.gameObject.SetActive(true);

                }
                
            }

            else
            {
                if (animals.gameObject.activeSelf == false)
                {
                    yield return null;
                }
                else
                {
                    animals.gameObject.SetActive(false);
                }

            }

        }
    }
}
