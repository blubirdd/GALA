using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    //[SerializeField] private CinemachineVirtualCamera[] _vcams = new CinemachineVirtualCamera[3];

    [SerializeField] private GameObject[] _cams;
    // Start is called before the first frame update
    #region Singleton

    public static CinemachineManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Cinemachine manageer found");
            return;
        }

        instance = this;
    }
    #endregion

    void Start()
    {
        GameEvents.instance.onDialogueStarted += SwitchToDialogueCam;

        GameEvents.instance.onCameraOpened += SwitchToFirstPersonCam;
        GameEvents.instance.onCameraClosed += SwitchToThirdPersonCam;

        //cutscene
        GameEvents.instance.onCutSceneEnter += SwitchToCutsceneCam;
    }

    public void SwitchToCutsceneCam()
    {
        //_vcams[0].Priority = 10;
        //_vcams[3].Priority = 11;
        _cams[0].SetActive(false);
        _cams[1].SetActive(false);
        _cams[2].SetActive(false);
        _cams[3].SetActive(true);
    }


    public void SwitchToFirstPersonCam()
    {
        //_vcams[0].Priority = 10;
        //_vcams[1].Priority = 11;
        _cams[0].SetActive(false);
        _cams[2].SetActive(true);
    }

    public void SwitchToDialogueCam()
    {
        //_vcams[0].Priority = 10;
        //_vcams[2].Priority = 11;
        _cams[1].SetActive(true);
        _cams[0].SetActive(false);
        _cams[2].SetActive(false);
        _cams[3].SetActive(false);
        StartCoroutine("WaitForDialogue");
    }

    public void SwitchToThirdPersonCam()
    {
        //_vcams[0].Priority = 11;
        //_vcams[1].Priority = 10;
        //_vcams[2].Priority = 10;
        _cams[0].SetActive(true);
        _cams[1].SetActive(false);
        _cams[2].SetActive(false);
        _cams[3].SetActive(false);
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
