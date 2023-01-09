using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _vcams = new CinemachineVirtualCamera[3];
    // Start is called before the first frame update


    void Start()
    {
        GameEvents.instance.onDialogueStarted += SwitchToDialogueCam;

        GameEvents.instance.onCameraOpened += SwitchToFirstPersonCam;
        GameEvents.instance.onCameraClosed += SwitchToThirdPersonCam;
    }

    public void SwitchToFirstPersonCam()
    {
        _vcams[0].Priority = 10;
        _vcams[1].Priority = 11;
    }

    public void SwitchToDialogueCam()
    {
        _vcams[0].Priority = 10;
        _vcams[2].Priority = 11;

        StartCoroutine("WaitForDialogue");
    }

    public void SwitchToThirdPersonCam()
    {
        _vcams[0].Priority = 11;
        _vcams[1].Priority = 10;
        _vcams[2].Priority = 10;
    }

    IEnumerator WaitForDialogue()
    {
        yield return new WaitUntil(() => DialogueSystem.dialogueEnded == true);
        SwitchToThirdPersonCam();
    }



    // Update is called once per frame
    void Update()
    {

    }
}
