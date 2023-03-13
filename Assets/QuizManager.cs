using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class QuizManager : MonoBehaviour
{
    [NonReorderable]
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int CurrentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreTxt;

    int totalQuestions = 0;
    public int score;

    [SerializeField] private int currentNumber =1;

    [SerializeField] TextMeshProUGUI numberUI;
    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion(); 
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        // When answer right
        score += 1;
        currentNumber +=1;
        UpdateNumberUI();
        QnA.RemoveAt(CurrentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        // When answer wrong
        QnA.RemoveAt(CurrentQuestion);
        generateQuestion();
        currentNumber +=1;
        UpdateNumberUI();

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
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[CurrentQuestion].Answers[i];

            if(QnA[CurrentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        Debug.Log(currentNumber);

        // for(int i =1; i<QnA.Count; i++)
        // {
        //     Debug.Log(i);
        // }
        if(QnA.Count > 0)
        {
            CurrentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[CurrentQuestion].Question;
        SetAnswers();

        }
        else
        {
            Debug.Log("Out of Question");
            GameOver();
        }
        
       
    }


}
