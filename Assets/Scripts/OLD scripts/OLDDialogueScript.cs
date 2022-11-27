using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OLDDialogueScript : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject controls;
    public bool dialogueDone = false;


    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        // gameObject.SetActive(true);
        DisableControls();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

	public void OnTouch(InputValue value)
	{
		 if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
	}

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text +=c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else
        {

            gameObject.SetActive(false);
            EnableControls();
            dialogueDone = true;
        }
    }

    void DisableControls()
    {

        controls.SetActive(false);

    }

    void EnableControls()
    {   
        controls.SetActive(true);
    }
}
