using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// Add this class to a platform to make it a jumping platform, a trampoline or whatever.
	/// It will automatically push any character that touches it up in the air.
	/// </summary>
	[AddComponentMenu("Corgi Engine/Environment/Jumper")]
	public class Jumper : MonoBehaviour 
	{
		/// the force of the jump induced by the platform
		public float JumpPlatformBoost = 40;
		public bool canmultiply=true;
		/// <summary>
		/// Triggered when a CorgiController touches the platform, applys a vertical force to it, propulsing it in the air.
		/// </summary>
		/// <param name="controller">The corgi controller that collides with the platform.</param>
			
		public virtual void OnTriggerStay2D(Collider2D collider)
		{
			CorgiController controller = collider.GetComponent<CorgiController> ();
			if (controller == null)
				return;
			if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				return;
			}

			float multiplier = 1f;
			if (Mathf.Abs (collider.GetComponent<Controls> ().LastFallingSpeed) > 1f) {
				multiplier = Mathf.Sqrt (Mathf.Abs (collider.GetComponent<Controls> ().LastFallingSpeed * 25f));
			} else {
				multiplier = 5f;
			}
			//print (multiplier);
			if (multiplier < 30f) {
				controller.SetVerticalForce (multiplier);
			} else {
				controller.SetVerticalForce (30f);
			}
		}

	}
}