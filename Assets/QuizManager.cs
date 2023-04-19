using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;
using DG.Tweening;

public class QuizManager : MonoBehaviour
{
    [NonReorderable]
    public List<QuestionAndAnswers> QnA;
    public List<QuestionAndAnswers> remaningQuestions;
    public GameObject[] options;
    public int CurrentQuestion;

    [Header("Panels")]
    public GameObject Quizpanel;
    //private CanvasGroup quizPanelCanvasGroup;
    public GameObject startPanel;
    public GameObject GoPanel;

    [Header("Buttons")]
    public GameObject finishButton;

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

    [Header("DOTWEEN")]
    private bool isMoving = false;
    public int numberOfQuestionsToDisplay;


    Character character;
    private void Start()
    {
        //originalPosition = Quizpanel.transform.position;
        //quizPanelCanvasGroup = Quizpanel.GetComponent<CanvasGroup>();
        totalQuestions = remaningQuestions.Count;
        remaningQuestions = new List<QuestionAndAnswers>(QnA);
        //currentNumber = 1;
        //UpdateNumberUI();
        //questionsDisplayed = 0;

        GoPanel.SetActive(false);
        Quizpanel.SetActive(false);
        generateQuestion();

        character = GetComponent<Character>();
    }

    public void retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GoPanel.SetActive(false);
        Quizpanel.SetActive(true);
        startPanel.SetActive(false);

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

        if (score <= 1)
        {
            finishButton.SetActive(false);
        }

        else
        {
            finishButton.SetActive(true);
        }
    }

    public void correct()
    {
        // When answer right
        score += 1;
        currentNumber +=1;
        UpdateNumberUI();
        remaningQuestions.RemoveAt(CurrentQuestion);
        generateQuestion();


    }

    public void wrong()
    {
        // When answer wrong
        remaningQuestions.RemoveAt(CurrentQuestion);
        generateQuestion();
        currentNumber +=1;
        UpdateNumberUI();

    }

    public void UpdateNumberUI()
    {
        bookJournal3D.SetActive(true);
        thoughtBubbleUI.SetActive(true);
        numberUI.text = currentNumber.ToString();

        //dotween
        if (isMoving) return;


        Sequence sequence = DOTween.Sequence();
        //sequence.Append(quizPanelCanvasGroup.DOFade(0f, 0.3f));
        sequence.Append(Quizpanel.transform.DOMoveX(Quizpanel.transform.position.x + 1000f, 0.5f));
        //quizPanelCanvasGroup.DOFade(1f, 0.3f);
        sequence.Append(Quizpanel.transform.DOMoveX(Quizpanel.transform.position.x, 0.3f));


        //set the flag to prevent multiple triggers
        isMoving = true;

        sequence.OnComplete(() => { isMoving = false; });

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

        //checks if there are questions left in the QnA list and if the number of questions displayed is less than 10
        if(remaningQuestions.Count > 0 && questionsDisplayed < numberOfQuestionsToDisplay)
        {
            CurrentQuestion = Random.Range(0, remaningQuestions.Count);
            QuestionTxt.text = remaningQuestions[CurrentQuestion].Question;
            SetAnswers();

            //increment the number of questions displayed
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
        //disable canvas

        Quizpanel.transform.parent.gameObject.SetActive(false);
        GoPanel.SetActive(false);
        Quizpanel.SetActive(false);
        startPanel.SetActive(true);

        bookJournal3D.SetActive(false);
        thoughtBubbleUI.SetActive(false);

        UIManager.instance.EnablePlayerMovement();
        ThirdPersonController.instance.SitUp();

        //disable quiz camera
        CinemachineManager.instance._cams[6].SetActive(false);

        //UI

        if(character != null)
        {
           TalkEvents.CharacterApproach(character); 
        }

    }


}
