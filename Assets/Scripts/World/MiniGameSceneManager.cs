using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneManager : MonoBehaviour
{
    public void LoadSnakeMiniGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Egg Game");
    }

    public void LoadWhackMiniGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Mole Minigame");
    }

    public void LoadChickenMiniGame()
    {
        Player.isfromChickenGame = true;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Runner");
    }
}
