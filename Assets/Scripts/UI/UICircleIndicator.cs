using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICircleIndicator : MonoBehaviour
{
    public float startingScale = 0.5f;
    public float endScale = 1f;
    public float fadeDuration = 1f;
    public float scaleDuration = 1f;
    public float delayBetweenLoops = 1f;
    public Ease easeType = Ease.OutSine;

    private Image circleImage;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        circleImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Set starting scale and alpha
        transform.localScale = new Vector3(startingScale, startingScale, 1f);
        canvasGroup.alpha = 1f;

        // Tween scale up to end scale
        transform.DOScale(endScale, scaleDuration).SetEase(easeType).OnComplete(() => {
            // Tween fade out
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(() => {
                // Set alpha back to 1 for next loop
                canvasGroup.alpha = 1f;
                // Tween scale back down to starting scale
                transform.localScale = new Vector3(startingScale, startingScale, 1f);
                // Repeat animation
                Start();
            });
        });
    }


    private void OnDestroy()
    {
        DOTween.Kill(rectTransform);
    }

}