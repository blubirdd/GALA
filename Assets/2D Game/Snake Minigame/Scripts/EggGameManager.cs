
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthBar healthBar;
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
        
    }

    public void BackToGame()
    {
        SceneManager.LoadSceneAsync("ForestStart");
    }
}
