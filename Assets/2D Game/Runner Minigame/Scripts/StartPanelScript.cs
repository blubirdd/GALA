using UnityEngine;
using UnityEngine.UI;

public class StartPanelScript : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        RunnerGameManager.instance.StartGame();
        gameObject.SetActive(false); // Hide the panel after starting the game
    }
}
