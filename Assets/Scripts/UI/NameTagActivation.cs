using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagActivation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _nameUICanvas;

    void Start()
    {
        _nameUICanvas.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _nameUICanvas.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _nameUICanvas.SetActive(false);
        }
    }

}
