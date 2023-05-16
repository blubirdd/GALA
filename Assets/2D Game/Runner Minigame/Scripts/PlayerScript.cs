
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;
    float score;

    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;

   Rigidbody2D RB;

   public TextMeshProUGUI ScoreTxt;

   private void Awake()
   {
        RB = GetComponent<Rigidbody2D>();
        score = 0;
   }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)) 
        // {
        //     if(isGrounded == true)
        //     {
        //         RB.AddForce(Vector2.up * JumpForce);
        //         isGrounded = false;
        //     }
        // }    
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch detected

            if (touch.phase == TouchPhase.Began && isGrounded)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
            }
        }
        
        if(isAlive)
        {
            score += Time.deltaTime * 4;
            ScoreTxt.text = "Score: " + score.ToString("F");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            if(isGrounded == false)
            {
                isGrounded = true;
            }
        }
    

    if(collision.gameObject.CompareTag("spike"))
        {
          isAlive = false;
          Time.timeScale = 0;
        }
    }

}
