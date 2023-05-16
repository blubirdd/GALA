using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    #region Singleton

    public static MainMenu instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MainMenu found");
            return;
        }

        instance = this;
    }
    #endregion

    private string tempPlayerName;
    public TMP_InputField nameInputField;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    [SerializeField] private GameObject playernameObject;
    [SerializeField] private TextMeshProUGUI playerNameUI;
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void GetPlayerName()
    {
        if(nameInputField.text == "")
        {
            tempPlayerName = "Juan";
        }

        else
        {
            tempPlayerName = nameInputField.text;

            Debug.Log(Player.playerName);
        }

        PlayerPrefs.SetString("name", tempPlayerName);
    }
    public void StartGame()
    {
        DisableAllButtons();

        //create  a new game
        DataPersistenceManager.instance.NewGame();

        Debug.Log("Starting new Game");

        DataPersistenceManager.instance.SaveGame();
        //load the scene with the load game from DatapersistenceManager.

        SceneManager.LoadSceneAsync("CharacterSelect");
        //SceneManager.LoadSceneAsync("StoryMode");
        //SceneManager.LoadSceneAsync("ForestStart");
        //SceneManager.LoadSceneAsync("GALA Demo");
    }

    public void OnContinueGame()
    {
        DisableAllButtons();

        DataPersistenceManager.instance.SaveGame();

        //load the scene
        SceneManager.LoadSceneAsync("StoryMode");
        //SceneManager.LoadSceneAsync("ForestStart");
        //SceneManager.LoadSceneAsync("GALA Demo");
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
        //if(data.playerName != "")
        //{
        //    Debug.Log("true");
        //    playernameObject.SetActive(true);
        //    playerNameUI.text = data.playerName;
        //}

        if (PlayerPrefs.GetString("name") != "")
        {
            playernameObject.SetActive(true);
            playerNameUI.text = PlayerPrefs.GetString("name");
        }
    }

    public void SaveData(GameData data)
    {
        //if(tempPlayerName != "")
        //{
        //    data.playerName = tempPlayerName;
        //}
    }
}
