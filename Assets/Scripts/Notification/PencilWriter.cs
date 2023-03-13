using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PencilWriter : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void WriteText()
    {
        // Save the initial text
        string initialText = _text.text;

        // Clear the text
        _text.text = "";

        StartCoroutine(TypeQuestName(initialText));
    }

    IEnumerator TypeQuestName(string name)
    {
        foreach (char letter in name.ToCharArray())
        {
            _text.text += letter;
            //anim speed
            yield return new WaitForSeconds(0.07f);

        }
    }
}

