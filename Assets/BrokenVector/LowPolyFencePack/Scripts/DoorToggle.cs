using System.Collections;
using UnityEngine;

namespace BrokenVector.LowPolyFencePack
{
    /// <summary>
    /// This class toggles the door animation.
    /// The gameobject of this script has to have the DoorController script which needs an Animator component
    /// and some kind of Collider which detects your mouse click applied.
    /// </summary>
    [RequireComponent(typeof(DoorController))]
	public class DoorToggle : MonoBehaviour, IInteractable
    {
        [Header("Interaction")]
        [SerializeField] private string _prompt;
        [SerializeField] private Sprite _icon;
        public string InteractionPrompt { get; set; }
        public Sprite icon { get; set; }

        private DoorController doorController;

        [SerializeField] private string requiredQuest;
        [SerializeField] private SubtleDialogueTrigger requiredDialogue;

        void Awake()
        {
            doorController = GetComponent<DoorController>();
            InteractionPrompt = _prompt;
            icon = _icon;

            if(requiredQuest == "")
            {
                StartCoroutine(WaitToToggle());
            }
        }

        IEnumerator WaitToToggle()
        {
            yield return new WaitForSeconds(1f);
            doorController.ToggleDoor();
        }

        public bool Interact(Interactor interactor)
        {
            if (requiredQuest == "" || Task.instance.tasksCompeleted.Contains(requiredQuest))
            {
                doorController.ToggleDoor();
            }

            else
            {
                requiredDialogue.TriggerDialogue();
            }

            return true;
        }

            //void OnMouseDown()
            //{
            //    doorController.ToggleDoor();
            //}

        }
}