using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterBodies;
    public GameObject male2Hat;
    private void Awake()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        for (int i = 0; i < characterBodies.Length; i++)
        {
            if(selectedCharacter == i)
            {
                characterBodies[i].gameObject.SetActive(true);

                Debug.Log("Character selected :" + i);
            }

            else
            {
                characterBodies[i].gameObject.SetActive(false);
            }
        }

        if (selectedCharacter == 3)
        {
            male2Hat.SetActive(true);
        }

        else
        {
            male2Hat.SetActive(false);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
