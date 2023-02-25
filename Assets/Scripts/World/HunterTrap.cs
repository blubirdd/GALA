using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTrap : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;

    public string InteractionPrompt => _prompt;
    public Sprite icon => _icon;

    public bool Interact(Interactor interactor)
    {

        this.gameObject.SetActive(false);
       return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
