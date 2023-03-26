using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnswerScript : MonoBehaviour
{
   
   public bool isCorrect = false;
   public QuizManager quizManager;

   private Image buttonImage;
   private Button button;
    private Color correctColor = new Color(0.235f, 0.906f, 0.239f, 1f);
    private Color wrongColor = new Color(1f, 0.486f, 0.486f, 1f);
    private Color normalColor = new Color(0.278f, 0.302f, 0.259f, 1f);

    private void Start()
   {

        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
   }
    public void Answer()
   {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");

            StartCoroutine(DelayWait());
            IEnumerator DelayWait()
            {
                buttonImage.color = correctColor;
                button.interactable = false;

                yield return new WaitForSeconds(1f);

                ThirdPersonController.instance.SitMoveHand();
                quizManager.correct();
                buttonImage.color = normalColor;

                button.interactable = true;
            }

        }
        else
        {
            Debug.Log("Wrong Answer");

            StartCoroutine(DelayWait());
            IEnumerator DelayWait()
            {
                buttonImage.color = wrongColor;
                button.interactable = false;

                yield return new WaitForSeconds(1f);

                ThirdPersonController.instance.SitMoveWrong();
                quizManager.wrong();
                buttonImage.color = normalColor;
                button.interactable = true;
            }
            
        }

   }


}
