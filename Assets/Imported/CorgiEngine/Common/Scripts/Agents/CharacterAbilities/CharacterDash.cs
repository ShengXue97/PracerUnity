using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Add this class to a character and it'll be able to perform a horizontal dash
	/// Animator parameters : Dashing
	/// </summary>
	[AddComponentMenu("Corgi Engine/Character/Abilities/Character Dash")] 
	public class CharacterDash : CharacterAbility
	{		
		/// This method is only used to display a helpbox text at the beginning of the ability's inspector
		public override string HelpBoxText() { return "This component allows your character to dash. Here you can define the distance the dash should cover, how much force to apply, and the cooldown between the end of a dash and the start of the next one."; }

		[Header("Dash")]
		/// the duration of dash (in seconds)
		public float DashDistance = 3f;
		/// the force of the dash
		public float DashForce = 40f;	
		/// the duration of the cooldown between 2 dashes (in seconds)
		public float DashCooldown = 1f;	
		public bool stunned;
		protected float _cooldownTimeStamp = 0;
		protected CharacterHorizontalMovement _characterHorizontalMovement;

		protected float _startTime ;
		protected Vector3 _initialPosition ;
		protected float _dashDirection;
		protected float _distanceTraveled = 0f;
		protected bool _shouldKeepDashing = true;
		protected float _computedDashForce;
		protected float _slopeAngleSave = 0f;
		protected bool _dashEndedNaturally = true;

		/// <summary>
		/// On Init we grab our CharacterHorizontalMovement component for future reference
		/// </summary>
		protected override void Initialization()
		{
			base.Initialization();
			_characterHorizontalMovement = GetComponent<CharacterHorizontalMovement>();
		}

		/// <summary>
		/// At the start of each cycle, we check if we're pressing the dash button. If we
		/// </summary>
		protected override void HandleInput()
		{
			
		}

		public void StunPlayer()
		{
			stunned = true;
			_movement.ChangeState (CharacterStates.MovementStates.Dashing);
			StartCoroutine (StopStunPlayer ());
			GetComponent<Controls> ().stunned = true;
			GetComponent<CharacterPause> ().PauseCharacter ();
		}

		public IEnumerator StopStunPlayer()
		{
			yield return new WaitForSeconds(3f);
			stunned = false;
			_movement.ChangeState (CharacterStates.MovementStates.Idle);
			GetComponent<CharacterPause> ().UnPauseCharacter ();
			GetComponent<Controls> ().stunned = false;

			//GetComponent<InputManager> ().enabled=true;		
		}
		/// <summary>
		/// The second of the 3 passes you can have in your ability. Think of it as Update()
		/// </summary>
		public override void ProcessAbility()
		{
			base.ProcessAbility();
		}

		/// <summary>
		/// Causes the character to dash or dive (depending on the vertical movement at the start of the dash)
		/// </summary>
		public virtual void StartDash()
		{						
						
		}

		/// <summary>
		/// Coroutine used to move the player in a direction over time
		/// </summary>
		protected virtual IEnumerator Dash() 
		{
			yield return null;
		}

		/// <summary>
		/// Adds required animator parameters to the animator parameters list if they exist
		/// </summary>
		protected override void InitializeAnimatorParameters()
		{
			RegisterAnimatorParameter("Dashing", AnimatorControllerParameterType.Bool);
		}

		/// <summary>
		/// At the end of the cycle, we update our animator's Dashing state 
		/// </summary>
		public override void UpdateAnimator()
		{
			MMAnimator.UpdateAnimatorBool(_animator,"Dashing",(_movement.CurrentState == CharacterStates.MovementStates.Dashing), _character._animatorParameters);
		}

	}
}