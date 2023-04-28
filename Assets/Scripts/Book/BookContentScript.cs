using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookContentScript : MonoBehaviour
{
    public GameObject[] pages;
    int index;
 
    SoundManager soundManager;
    private void Start()
    {
        index = 0;
        pages[0].gameObject.SetActive(true);
        /*for(int i = 1; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }*/

        soundManager = SoundManager.instance;
    }

    public void ButtonNext()
    {
        soundManager.PlaySoundFromClips(4);
        if (index < pages.Length - 1)
        {
            index += 1;
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(false);
                pages[index].gameObject.SetActive(true);
            }
        }
 
    }

    public void ButtonPrevious()
    {
        soundManager.PlaySoundFromClips(4);
        if (index != 0)
        {
            index -= 1;
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].gameObject.SetActive(false);
                pages[index].gameObject.SetActive(true);
            }
          
        }

        if (index == 0)
        {
            pages[0].gameObject.SetActive(true);
        }
    }
}
