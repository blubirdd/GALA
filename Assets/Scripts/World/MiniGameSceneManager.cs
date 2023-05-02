using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSceneManager : MonoBehaviour
{
    public void LoadSnakeMiniGame()
    {
        SceneManager.LoadScene("Egg Game");
    }
}
