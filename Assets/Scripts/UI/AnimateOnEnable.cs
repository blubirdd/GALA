using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AnimateOnEnable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool fadeOnDisable = true;
    
    private void OnEnable()
    {
        //animate 
        transform.localPosition = new Vector3(0, -500, 0);
        this.GetComponent<CanvasGroup>().alpha = 0;

        transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        this.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetUpdate(true);
    }

    public void AnimateOnDisable()
    {
        // Animate out
        if (fadeOnDisable)
        {
            transform.DOLocalMoveY(-500, 0.3f).SetEase(Ease.InBack).SetUpdate(true);
            this.GetComponent<CanvasGroup>().DOFade(0, 0.3f).SetUpdate(true).OnComplete(() => {
                this.gameObject.SetActive(false);
            });

        }

        else
        {
            transform.DOLocalMoveY(-500, 0.3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {
                this.gameObject.SetActive(false); 
            });
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
