using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public void continuebutton()
    {
        Debug.Log("continuebutton");
        MainMenuButtonSounds.Instance.PlaySound("continuebutton", 1f);
    }
    public void newgamebutton()
    {
        Debug.Log("newgamebutton");
        MainMenuButtonSounds.Instance.PlaySound("newgamebutton", 1f);
    }
    public void settingbutton()
    {
        Debug.Log("settingbutton");
        MainMenuButtonSounds.Instance.PlaySound("settingbutton", 1f);
    }
    public void quitbutton()
    {
        Debug.Log("quitbutton");
        MainMenuButtonSounds.Instance.PlaySound("quitbutton", 1f);
    }
}