using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoleGameManager : MonoBehaviour {
  [SerializeField] private List<Mole> moles;

    [Header("UI objects")]
    [SerializeField] private GameObject canvas;
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject outOfTimeText;
  [SerializeField] private GameObject bombText;
  [SerializeField] private TMPro.TextMeshProUGUI timeText;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;

  // Hardcoded variables you may want to tune.
  private float startingTime = 30f;

  // Global variables
  private float timeRemaining;
  private HashSet<Mole> currentMoles = new HashSet<Mole>();

  private bool playing = false;

    [Header("Audio")]
    [SerializeField] private AudioSource whackSound;
    public GameObject hammerPrefab;
    [Header("Player Score")]
    public GameObject successPanel;
    public float scoreNeeded = 30;
    [SerializeField] private TextMeshProUGUI mainScore;
    [SerializeField] private TextMeshProUGUI otherScore;
  public int score;
    // This is public so the play button can see it.
    public void StartGame() {
    // Hide/show the UI elements we don't/do want to see.
    playButton.SetActive(false);
    outOfTimeText.SetActive(false);
    bombText.SetActive(false);
    gameUI.SetActive(true);
     successPanel.SetActive(false);
    // Hide all the visible moles.
    for (int i = 0; i < moles.Count; i++) {
      moles[i].Hide();
      moles[i].SetIndex(i);
    }
    // Remove any old game state.
    currentMoles.Clear();
    // Start with 30 seconds.
    timeRemaining = startingTime;
    score = 0;
    scoreText.text = "0";
    playing = true;
  }

  public void GameOver(int type) {
    // Show the message.
    if (type == 0) {
      if(score < scoreNeeded)
            {
                outOfTimeText.SetActive(true);
            }
            else
            {
                successPanel.SetActive(true);
            }
      
    } else {

            if (score < scoreNeeded)
            {
                bombText.SetActive(true);
            }

            else
            {
                successPanel.SetActive(true);
            }

    }
    // Hide all moles.
    foreach (Mole mole in moles) {
      mole.StopGame();
    }
    // Stop the game and show the start UI.
    playing = false;
    //playButton.SetActive(true);

        Debug.Log("Score is " + score);

        mainScore.text = score.ToString();

        if(otherScore != null)
        {
            otherScore.text = score.ToString();
        }

    
  }

  // Update is called once per frame
  void Update() {
    if (playing) {
      // Update time.
      timeRemaining -= Time.deltaTime;
      if (timeRemaining <= 0) {
        timeRemaining = 0;
        GameOver(0);
      }
      timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
      // Check if we need to start any more moles.
      if (currentMoles.Count <= (score / 10)) {
        // Choose a random mole.
        int index = Random.Range(0, moles.Count);
        // Doesn't matter if it's already doing something, we'll just try again next frame.
        if (!currentMoles.Contains(moles[index])) {
          currentMoles.Add(moles[index]);
          moles[index].Activate(score / 10);
        }
      }
    }
  }

  public void AddScore(int moleIndex) {
    // Add and update score.
    score += 1;
    scoreText.text = $"{score}";
    // Increase time by a little bit.
    timeRemaining += 1;

    whackSound.Play();
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f; // Distance from camera
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(30, 0,0));
            spawnPosition.z = 0.0f; // Make sure the hammer is at the same z position as the canvas
            GameObject hammer = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity, canvas.transform);
            Destroy(hammer, 0.5f); // Destroy the hammer after 0.5 seconds
        }
    }

  public void Missed(int moleIndex, bool isMole) {
    if (isMole) {
      // Decrease time by a little bit.
      timeRemaining -= 2;
    }
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);
  }

  public void DisplayScore()
    {

    }
}
