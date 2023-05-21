using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    float score;

    bool isGrounded = false;
    bool isAlive = true;

    Rigidbody2D rb;

    public TextMeshProUGUI scoreTxt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        score = 0;
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
            score += Time.deltaTime * 4;
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
        isAlive = false;
        RunnerGameManager.instance.GameOver();
    }
}
