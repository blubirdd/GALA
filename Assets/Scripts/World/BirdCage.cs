using Cinemachine;
using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCage : MonoBehaviour, IInteractable
{
    public GameObject animalInside;

    [Header("Interaction Interface")]
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;
    
    public string InteractionPrompt { get; set; }
    public Sprite icon { get; set; }


    [Header("Animation")]
    public GameObject doorCage;

    [Header("Settings")]
    public bool isSmallCage;
    public bool isOpen;
    Character character;
    void Start()
    {
        icon = _icon;
        InteractionPrompt = _prompt;

        character = GetComponent<Character>();

        isOpen = false;
    }
    public bool Interact(Interactor interactor)
    {

        if (!isOpen)
        {
            ThirdPersonController.instance.ItemPickupAnim();
            if (isSmallCage)
            {
                doorCage.transform.DOMoveY(doorCage.transform.position.y + 1f, 2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    ParticleManager.instance.SpawnPuffParticle(transform.position);
                    animalInside.SetActive(false);
                });
            }

            else
            {
                doorCage.transform.DORotate(new Vector3(0f, 90f, 0f), 1.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    ParticleManager.instance.SpawnPuffParticle(doorCage.transform.position);
                    animalInside.SetActive(false);
                });
            }

            isOpen = true;

            //trigger Quest
            if(character != null)
            {
                TriggerQuest();
            }


            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        return true;
    }

    public void TriggerQuest()
    {
        StartCoroutine(WaitForAnimation());
        IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(2f);
            TalkEvents.CharacterApproach(character);
        }
        
    }

}
