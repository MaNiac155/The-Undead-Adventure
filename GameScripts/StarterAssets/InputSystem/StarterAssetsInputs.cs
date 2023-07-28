using UnityEngine;
#if ENABLE_INPUT_SYSTEM
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
		public bool roll;
        public bool punch;
        public bool heavy;
        public bool block;
        public bool guard;
        public bool doge;
		public bool changeweapon;
		public bool recover;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
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

		public void OnRoll(InputValue value)
		{
			RollInput(value.isPressed);
		}

        public void OnPunch(InputValue value)
        {
            PunchInput(value.isPressed);
        }

        public void OnHeavy(InputValue value)
        {
            HeavyInput(value.isPressed);
        }
        public void OnBlock(InputValue value)
        {
            BlockInput(value.isPressed);
        }

        public void OnGuard(InputValue value)
        {
            GuardInput(value.isPressed);
        }

        public void OnDoge(InputValue value)
        {
            DogeInput(value.isPressed);
        }

		public void OnChangeWeapon(InputValue value)
		{
            ChangeWeaponInput(value.isPressed);
        }

        public void OnRecover(InputValue value)
        {
            RecoverInput(value.isPressed);
        }

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void RollInput(bool newRoll)
		{
			roll = newRoll;

        }

        public void DogeInput(bool newDoge)
        {
            doge = newDoge;

        }

        public void PunchInput(bool newPunch)
        {
            punch = newPunch;

        }

        public void HeavyInput(bool newHeavy)
        {
            heavy = newHeavy;

        }

        public void BlockInput(bool newBlock)
        {
            block = newBlock;

        }

        public void GuardInput(bool newBlock)
        {
            guard = newBlock;

        }

        public void ChangeWeaponInput(bool newChangeWeapon)
        {
            changeweapon = newChangeWeapon;

        }

        public void RecoverInput(bool newRecover)
		{
			recover = newRecover;
		}

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