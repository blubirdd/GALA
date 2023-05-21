using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerGameManager : MonoBehaviour
{
    #region Singleton

    public static RunnerGameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of RunnerGameManager found");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject gameOverPanel;
    public GameObject loadingScreen;

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void BackToGame()
    {
        loadingScreen.SetActive(true);

        SceneManager.LoadSceneAsync("StoryMode");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        PauseGame();
    }
}