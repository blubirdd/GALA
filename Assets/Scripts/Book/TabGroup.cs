using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public TabButton selectedTab;

    public List<GameObject> objectsToSwap;

    [Header("Colors")]
    public Color initialColor;
    public Color selectedColor;

    public bool changeColor = true;
    public bool isBook = false;
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        if (isBook)
        {
            SoundManager.instance.PlaySoundFromClips(4);
        }

        ResetTabs();
       
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void onTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();

        //set color
        if (changeColor)
        {
            button.GetComponent<Image>().color = selectedColor;
        }

        else
        {
            button.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }


        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab)
            {
                continue;
            }

            if (changeColor)
            {
                button.GetComponent<Image>().color = initialColor;
            }

            else
            {
                button.transform.localScale = new Vector3(1f, 1f, 1f);
            }

        }
    }
}
