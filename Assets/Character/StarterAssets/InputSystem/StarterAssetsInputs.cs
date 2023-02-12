using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
        public bool crouch;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);

        }

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        //public void OnBack(InputValue value)
        //{
        //   PauseInput(value.isPressed);
        //}

		//for pc add on crouch

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
            sprint = newMoveDirection.sqrMagnitude > 1f;
            move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
           // Debug.Log("PLAYER JUMP");
        }

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void CrouchInput(bool newCrouchstate)
		{
			crouch = newCrouchstate;
		}

		//public void PauseInput(bool newPauseState)
		//{
		//	UIManager.instance.OpenPauseMenu();
		//	Debug.Log("Opened Pause Menu through back button");
		//}



		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}



		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}