using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragSystem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject parent;

    Vector3 offset;
    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";
    private Color originalColor;
    private Vector3 originalPosition;

    private Image image;

    public bool isCorrect;

    void Start()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();

        image = GetComponent<Image>();
        originalColor = image.color;
    }

    void OnEnable()
    {
        StartCoroutine(WaitForShuffle());
        IEnumerator WaitForShuffle()
        {
            yield return new WaitForEndOfFrame();
            originalPosition = transform.position;
            Time.timeScale = 0f;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Input.mousePosition;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        if (raycastResult.gameObject?.tag == destinationTag)
        {
            transform.position = raycastResult.gameObject.transform.position;
            if (isCorrect)
            {
                image.color = Color.green;
                StartCoroutine(ExitOut());
            }
            else
            {
                image.color = Color.red;
                transform.position = originalPosition;
                StartCoroutine(TurnOriginalColor());
            }
        }

        else
        {
            transform.position = originalPosition;
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    void OnDisable()
    {
        transform.position = originalPosition;
        image.color = originalColor;
        isCorrect = false;
    }

    IEnumerator TurnOriginalColor()
    {
        yield return new WaitForSecondsRealtime(1f);
        image.color = originalColor;
    }

    IEnumerator ExitOut()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        parent.SetActive(false);
    }
}
