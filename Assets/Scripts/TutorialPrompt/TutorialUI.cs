using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    #region Singleton

    public static TutorialUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of TutorialUI found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject[] tutorialPrefabs;
    // Start is called before the first frame update
    public GameObject objectParentToInstantiate;
    public void EnableTutorial(int index)
    {
        UIManager.instance.DisablePlayerMovement();
        Instantiate(tutorialPrefabs[index], objectParentToInstantiate.transform);

        Time.timeScale = 0f;


    }

    private void Start()
    {
        StartCoroutine(Testing());
        IEnumerator Testing()
        {
            yield return new WaitForSeconds(5);
            EnableTutorial(0);

        }
    }

}
