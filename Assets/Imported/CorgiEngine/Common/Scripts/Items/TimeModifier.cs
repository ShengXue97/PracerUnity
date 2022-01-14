using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace MoreMountains.CorgiEngine
{
	/// <summary>
	/// Add this to an item to make it modify time when it gets picked up by a Character
	/// </summary>
	[AddComponentMenu("Corgi Engine/Items/Time Modifier")]
	public class TimeModifier : MonoBehaviour
	{
		/// the effect to instantiate when picked up
		public GameObject Effect;
		/// the time speed to apply while the effect lasts
		public float TimeSpeed = 0.5f;
		/// how long the duration will last , in seconds
		public float Duration = 1.0f;
		public int FreezeTimeNum;

		protected WaitForSeconds _changeTimeWFS;

		void Update()
		{
			if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor"))) {
				// we send a new time scale event for the GameManager to catch (and other classes that may listen to it too)
				MMEventManager.TriggerEvent (new CorgiEngineTimeScaleEvent (TimeScaleMethods.Set, 1f, 0f));
				Camera.main.GetComponent<CameraFollow> ().TimeScale = 1f;
				GUIManager.Instance.SetTimeSplash (false);
				Camera.main.GetComponent<CameraFollow> ().FreezeEffect.SetActive (false);
			}
		}
	    /// <summary>
	    /// Triggered when something collides with the TimeModifier
	    /// </summary>
	    /// <param name="collider">The object that collide with the TimeModifier</param>
		public void FreezeTime () 
		{
			if (Camera.main.GetComponent<CameraFollow>().editplay == 0) {
				return;
			}
			_changeTimeWFS = new WaitForSeconds (Duration * TimeSpeed);

			// we start the ChangeTime coroutine
			StartCoroutine (ChangeTime ());

			// adds an instance of the effect at the TimeModifier's position
			Instantiate(Effect,transform.position,transform.rotation);
			Camera.main.GetComponent<CameraFollow> ().FreezeEffect.SetActive (true);
		}

	    /// <summary>
	    /// Asks the Game Manager to change the time scale for a specified duration.
	    /// </summary>
	    /// <returns>The time.</returns>
	    protected virtual IEnumerator ChangeTime()
		{
			// we send a new time scale event for the GameManager to catch (and other classes that may listen to it too)
			MMEventManager.TriggerEvent (new CorgiEngineTimeScaleEvent (TimeScaleMethods.Set, TimeSpeed, 0f));
			Camera.main.GetComponent<CameraFollow> ().TimeScale = TimeSpeed;
			GUIManager.Instance.SetTimeSplash (true);
			// we multiply the duration by the timespeed to get the real duration in seconds
			yield return _changeTimeWFS;

			//Check if any new time modifier has been activated. If yes, ignore this call
			if (Camera.main.GetComponent<CameraFollow> ().FreezeTimeNum == FreezeTimeNum) {
				// we send a new time scale event for the GameManager to catch (and other classes that may listen to it too)
				MMEventManager.TriggerEvent (new CorgiEngineTimeScaleEvent (TimeScaleMethods.Set, 1f, 0f));
				Camera.main.GetComponent<CameraFollow> ().TimeScale = 1f;
				GUIManager.Instance.SetTimeSplash (false);
				Camera.main.GetComponent<CameraFollow> ().FreezeEffect.SetActive (false);
			}
		}


	}
}