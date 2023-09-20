using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NameTagActivation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _nameUICanvas;

    [Header("Animation")]
    [SerializeField] private float scaleDuration = 1.5f;

    void Start()
    {
        if(_nameUICanvas != null)
        _nameUICanvas.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(_nameUICanvas != null)
            {
                _nameUICanvas.SetActive(true);
                _nameUICanvas.transform.localScale = Vector3.zero;
                _nameUICanvas.transform.DOScale(Vector3.one, scaleDuration).SetEase(Ease.OutBack);
            }

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_nameUICanvas != null)
            {
                _nameUICanvas.transform.DOScale(Vector3.zero, scaleDuration).SetEase(Ease.InBack).OnComplete(() => {
                    // set to false
                    _nameUICanvas.SetActive(false);
                });
            }

           
        }
    }

}
