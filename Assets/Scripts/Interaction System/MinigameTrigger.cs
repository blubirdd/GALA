using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    #region Singleton

    public static MinigameTrigger instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MinigameTrigger found");
            return;
        }

        instance = this;
    }
    #endregion

    //public GameObject mainSceneEventSystem;
    //public GameObject gameCamera;
    //public GameObject playerPack;
    //public void LoadSceneAdditive(string sceneName)
    //{
    //    UIManager.instance.DisablePlayerMovement();
    //    UIManager.instance.PauseGame();
    //    playerPack.SetActive(false);
    //    //mainSceneEventSystem.SetActive(false);
    //    //gameCamera.SetActive(false);


    //    StartCoroutine(WaitForSeconds());
    //    IEnumerator WaitForSeconds()
    //    {
    //        yield return new WaitForEndOfFrame();
    //        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    //    }


    //}

    public GameObject timeline;

    public void LoadScene(string sceneName, float delay)
    {

        StartCoroutine(WaitForDelay());
        IEnumerator WaitForDelay()
        {
            timeline.SetActive(true);
            UIManager.instance.DisablePlayerMovement();
            yield return new WaitForSeconds(delay);

            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(sceneName);
        }
       
    }
}
