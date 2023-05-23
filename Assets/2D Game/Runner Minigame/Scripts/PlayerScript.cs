using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour, IDataPersistence
{
    public float jumpForce;
    float score;

    bool isGrounded = false;
    bool isAlive = true;

    Rigidbody2D rb;

    public TextMeshProUGUI scoreTxt;

    Animator animator;

    public AudioSource music;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetFloat("AnimSpeed", 1f);
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch detected

            if (touch.phase == TouchPhase.Began && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
        }
        if (isAlive)
        {
            score += Time.deltaTime * 1;
            scoreTxt.text = "Score: " + score.ToString("F");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
            }
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            GameOver();
        }
    }


    void GameOver()
    {
        music.Stop();
        isAlive = false;
        RunnerGameManager.instance.GameOver();
    }

    public void LoadData(GameData data)
    {
       
    }

    public void SaveData(GameData data)
    {
        data.chickenGameScore = Mathf.RoundToInt(score);
    }

    public void GotoStory()
    {
        SceneManager.LoadSceneAsync("StoryMode");
    }
}
