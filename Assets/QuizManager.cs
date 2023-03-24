using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;

public class QuizManager : MonoBehaviour
{
    [NonReorderable]
    public List<QuestionAndAnswers> QnA;
    public List<QuestionAndAnswers> remaningQuestions;
    public GameObject[] options;
    public int CurrentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreTxt;

    int totalQuestions = 0;
    int questionsDisplayed = 0;
    public int score;

    [SerializeField] private int currentNumber = 1;

    [SerializeField] TextMeshProUGUI numberUI;

    [Header("UI AND 3D Elements")]
    public GameObject bookJournal3D;
    public GameObject thoughtBubbleUI;
    private void Start()
    {
        totalQuestions = remaningQuestions.Count;
        remaningQuestions = new List<QuestionAndAnswers>(QnA);
        //currentNumber = 1;
        //UpdateNumberUI();
        //questionsDisplayed = 0;

        GoPanel.SetActive(false);
        generateQuestion();

        
    }

    public void retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GoPanel.SetActive(false);
        Quizpanel.SetActive(true);

        remaningQuestions.Clear();

        remaningQuestions = new List<QuestionAndAnswers>(QnA);
        totalQuestions = remaningQuestions.Count;

        //reset
        

        currentNumber = 1;
        UpdateNumberUI();
        questionsDisplayed = 0;
        score = 0;





        generateQuestion();

    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + questionsDisplayed;
    }

    public void correct()
    {
        // When answer right
        score += 1;
        currentNumber +=1;
        UpdateNumberUI();
        remaningQuestions.RemoveAt(CurrentQuestion);
        generateQuestion();

        Debug.Log("CORRECT");
        ThirdPersonController.instance.SitMoveHand();
    }

    public void wrong()
    {
        // When answer wrong
        remaningQuestions.RemoveAt(CurrentQuestion);
        generateQuestion();
        currentNumber +=1;
        UpdateNumberUI();

        Debug.Log("WRONG!");
        ThirdPersonController.instance.SitMoveWrong();
    }

    public void UpdateNumberUI()
    {
        numberUI.text = currentNumber.ToString();

        
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = remaningQuestions[CurrentQuestion].Answers[i];

            if(remaningQuestions[CurrentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        //Enable UI
        //bookJournal3D.SetActive(true);
        //thoughtBubbleUI.SetActive(true);

        // Checks if there are questions left in the QnA list and if the number of questions displayed is less than 10
        if(remaningQuestions.Count > 0 && questionsDisplayed < 10)
        {
            CurrentQuestion = Random.Range(0, remaningQuestions.Count);
            QuestionTxt.text = remaningQuestions[CurrentQuestion].Question;
            SetAnswers();

            // Increment the number of questions displayed
            questionsDisplayed++;
        }
        else
        {
            Debug.Log("Out of Question");
            GameOver();
        }
    }

    public void Finish()
    {
        GoPanel.SetActive(false);
        Quizpanel.SetActive(false);

        bookJournal3D.SetActive(false);
        thoughtBubbleUI.SetActive(false);

        UIManager.instance.EnablePlayerMovement();
        ThirdPersonController.instance.SitUp();

        //disable quiz camera
        CinemachineManager.instance._cams[6].SetActive(false);

        //UI

    }


}
