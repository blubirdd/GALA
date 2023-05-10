using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem ;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1f;
    [SerializeField] private LayerMask _interactableMask;
    public InteractionPromptUI _interactionPromptUI;


    [SerializeField] private Button _interactionButton = null;
    private bool _buttonWasPressed;


    private readonly Collider[] _colliders = new Collider[1];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;
    private Outline _outline;


    private void Start()
    {
        _interactionButton.onClick.AddListener(ButtonPressed);
        _buttonWasPressed = false;

    }

    private bool _wasEnabled = false;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask, QueryTriggerInteraction.Ignore);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _outline = _colliders[0].GetComponent<Outline>();

            if (_interactable != null)
            {
                if (!_interactionPromptUI.isDisplayed)
                {
                    _interactionPromptUI.Setup(_interactable.InteractionPrompt, _interactable.icon);
                }

                //if (Keyboard.current.eKey.wasPressedThisFrame)
                //{
                //    _interactable.Interact(this);
                //}

                if (_buttonWasPressed == true)
                {
                    _interactable.Interact(this);
                    _buttonWasPressed = false;
                }
            }

            if (_outline != null)
            {
                if (!_outline.enabled && !_wasEnabled)
                {
                    _outline.enabled = true;
                    _wasEnabled = true;
                }
            }
        }
        else
        {
            if (_wasEnabled && _outline != null)
            {
                _outline.enabled = false;
                _wasEnabled = false;
            }

            if (_interactable != null)
            {
                _interactable = null;
            }

            if (_interactionPromptUI.isDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }
    }

    private void ButtonPressed()
    {
        _buttonWasPressed = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
