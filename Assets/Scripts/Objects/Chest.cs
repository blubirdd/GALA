using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Sprite _icon;
        public string InteractionPrompt { get; set; }
        public Sprite icon { get; set; }


    private Animator animator;
    private NameTagActivation _activation;

    void Start()
    {
        _activation = GetComponent<NameTagActivation>();
        animator = GetComponent<Animator>();
        InteractionPrompt = _prompt;
        icon = _icon;
    }
    public bool Interact(Interactor interactor)
    {
  
       animator.SetBool("open", true);
       Debug.Log("Chest Opened");

        ParticleManager.instance.SpawnChestOpenParticle(transform.position);
        //SoundManager.instance.PlaySoundFromClips(13);
        //disable interaction

        if(_activation != null)
        {
            _activation._nameUICanvas.SetActive(false);

        }

        gameObject.layer = default;

       return true;
    }

}
