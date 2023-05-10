
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggGameManager : MonoBehaviour
{
    #region Singleton

    public static EggGameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EggGameManager found");
            return;
        }

        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    public HealthBar healthBar;
    public GameObject gameOverPanel;

    public GameObject loadingScreen;
    void Start()
    {
        Time.timeScale = 0f;   
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        healthBar.health = 100;
        ClockManager.instance.StartClockIndependent(60);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(healthBar.health == 0)
        {
            gameOverPanel.SetActive(true);
            EggPause.instance.PauseGame();
        }
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void SpawnHit()
    {

    }
    public void BackToGame()
    {
        Player.instance.eggGameScore = 10;
        SwampEvents.fromEggGame = true;
        loadingScreen.SetActive(true);

        SceneManager.LoadSceneAsync("StoryMode");
        //SceneManager.LoadSceneAsync("ForestStart");
    }

    //public void LoadData(GameData data)
    //{
       
    //}

    //public void SaveData(GameData data)
    //{
    //    data.eggGameScore = Player.instance.eggGameScore;
    //}
}
