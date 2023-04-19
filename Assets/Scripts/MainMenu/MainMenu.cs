using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    private string tempPlayerName;
    public TMP_InputField nameInputField;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void GetPlayerName()
    {
        tempPlayerName = nameInputField.text;
        Debug.Log(Player.playerName);
    }
    public void StartGame()
    {
        DisableAllButtons();

        //create  a new game
        DataPersistenceManager.instance.NewGame();

        Debug.Log("Starting new Game");

        DataPersistenceManager.instance.SaveGame();
        //load the scene with the load game from DatapersistenceManager.
        SceneManager.LoadSceneAsync("ForestStart");
    }

    public void OnContinueGame()
    {
        DisableAllButtons();

        DataPersistenceManager.instance.SaveGame();

        //load the scene
        SceneManager.LoadSceneAsync("ForestStart");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableAllButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(GameData data)
    {
        data.playerName = tempPlayerName;
    }
}
