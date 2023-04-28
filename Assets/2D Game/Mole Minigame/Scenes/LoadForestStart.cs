using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadForestStart : MonoBehaviour
{
    public MoleGameManager moleGameManager;
    // Start is called before the first frame update
 public void LoadSceneForest()
    {
        if(Player.instance != null)
        {
            Player.instance.moleGameScore = moleGameManager.score;
        }

        SwampEvents.fromMoleGame = true;
        SceneManager.LoadScene("ForestStart");
    }
}
