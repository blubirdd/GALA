using DG.Tweening;
using TMPro;
using UnityEngine;

public class PencilWriter : MonoBehaviour
{
    [SerializeField] private float writeDuration = 2f;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void WriteText()
    {
        // Save the initial text
        string initialText = _text.text;

        // Clear the text and set alpha to 0
        _text.text = "";

        // Create a sequence to animate the text
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < initialText.Length; i++)
        {
            int index = i;
            char character = initialText[index];
            sequence.AppendCallback(() =>
            {
                _text.text += character;
            });
            sequence.Append(_text.DOFade(1, writeDuration / initialText.Length));
            sequence.AppendInterval(0.1f);
        }

        // Play the sequence
        sequence.Play();
    }
}

