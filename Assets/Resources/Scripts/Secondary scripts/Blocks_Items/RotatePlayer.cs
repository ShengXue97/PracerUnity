using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Adding this script to a trigger, volume box collider2D will make sure that any character that enters it with a CharacterGravity ability on itself will
/// have its "reverse input when upside down" property set to false. This was created for the purposes of the FeaturesGravity demo but feel free to use it in your game if needed.
/// </summary>
public class RotatePlayer : MonoBehaviour 
{
	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (Camera.main.GetComponent<CameraFollow>().editplay == 0  && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			return;
		}
		GameObject other = collider.gameObject;
		string blocktag = gameObject.name;
		CharacterGravity characterGravity = collider.gameObject.GetComponent<CharacterGravity> ();
		CharacterWallClinging characterwallclinging = collider.gameObject.GetComponentNoAlloc<CharacterWallClinging> ();
		if (characterGravity == null) {
			return;
		} else {
			if (GetComponent<SnapToGrid> ().colside == "bottom" && Camera.main.GetComponent<CameraFollow> ().PauseGame==false) {
				if (blocktag == "antigravity") {
					Camera.main.GetComponent<CameraFollow> ().pausegame ();
					FreezePlayer (other);
					StartCoroutine (Rotate180 (collider.gameObject, 0.003f, "180", 0f));
				}
				else if (blocktag == "rotateright") {
					Camera.main.GetComponent<CameraFollow> ().pausegame ();
					FreezePlayer (other);
					StartCoroutine (Rotate (collider.gameObject, 0.003f, "right", 0f));
				}
				else if (blocktag == "rotateleft") {
					Camera.main.GetComponent<CameraFollow> ().pausegame ();
					FreezePlayer (other);
					StartCoroutine (Rotate (collider.gameObject, 0.003f, "left", 0f));
				}
			}
		}
	}

	IEnumerator Rotate180(GameObject obj,float time, string direction, float rotation)
	{
		CharacterGravity characterGravity = obj.GetComponent<CharacterGravity> ();
		CharacterPause characterPause = obj.GetComponent<CharacterPause> ();
		yield return new WaitForSeconds (time);
		// Code to execute after the delay
		if (direction == "180") {
			if (rotation <= 179f) {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				} else {
					rotation += 1f;
					Camera.main.GetComponent<CameraFollow> ().UICanvas.GetComponent<RectTransform> ().Rotate (new Vector3 (0f, 0f, 1f));
					Camera.main.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, 1f));

					characterGravity.SetGravityAngle (characterGravity.GravityAngle + 1f);
					StartCoroutine (Rotate180 (obj, time, "180", rotation));
				}
			} else {
				if (obj.GetComponent<Controls> ().protation == 0f) {
					obj.GetComponent<Controls> ().protation =180f;
				}
				else if (obj.GetComponent<Controls> ().protation == 90f) {
					obj.GetComponent<Controls> ().protation =270f;
				}
				else if (obj.GetComponent<Controls> ().protation == 180f) {
					obj.GetComponent<Controls> ().protation =0f;
				}
				else if (obj.GetComponent<Controls> ().protation == 270f) {
					obj.GetComponent<Controls> ().protation =90f;
				}

				Camera.main.GetComponent<CameraFollow> ().unpausegame ();
				characterPause.UnPauseCharacter ();
				UnFreezePlayer (obj);
				GetComponent<SnapToGrid> ().colside = "top";

			}
		}


	}

	IEnumerator Rotate(GameObject obj,float time, string direction, float rotation)
	{
		CharacterGravity characterGravity = obj.GetComponent<CharacterGravity> ();
		CharacterPause characterPause = obj.GetComponent<CharacterPause> ();
		Character characterControls = obj.GetComponent<Character> ();
		yield return new WaitForSeconds (time);
		// Code to execute after the delay
		if (direction == "right") {
			if (rotation <= 89f) {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				} else {
					rotation += 1f;
					Camera.main.GetComponent<CameraFollow> ().UICanvas.GetComponent<RectTransform> ().Rotate (new Vector3 (0f, 0f, 1f));
					Camera.main.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, 1f));
					characterGravity.SetGravityAngle (characterGravity.GravityAngle + 1f);
					StartCoroutine (Rotate (obj, time, direction, rotation));
				}
			}else {
				if (obj.GetComponent<Controls> ().protation == 0f) {
					obj.GetComponent<Controls> ().protation =90f;
				}
				else if (obj.GetComponent<Controls> ().protation == 90f) {
					obj.GetComponent<Controls> ().protation =180f;
				}
				else if (obj.GetComponent<Controls> ().protation == 180f) {
					obj.GetComponent<Controls> ().protation =270f;
				}
				else if (obj.GetComponent<Controls> ().protation == 270f) {
					obj.GetComponent<Controls> ().protation =0f;
				}
				GetComponent<SnapToGrid> ().colside = "left";

				Camera.main.GetComponent<CameraFollow> ().unpausegame ();
				characterPause.UnPauseCharacter ();
				UnFreezePlayer (obj);

			}
		} else if (direction == "left") {
			if (rotation <= 89f) {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
				} else {
					rotation += 1f;
					Camera.main.GetComponent<CameraFollow> ().UICanvas.GetComponent<RectTransform> ().Rotate (new Vector3 (0f, 0f, -1f));
					Camera.main.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, -1f));
					characterGravity.SetGravityAngle (characterGravity.GravityAngle - 1f);
					StartCoroutine (Rotate (obj, time, direction, rotation));
				}
			} else {
				if (obj.GetComponent<Controls> ().protation == 0f) {
					obj.GetComponent<Controls> ().protation =270f;
				}
				else if (obj.GetComponent<Controls> ().protation == 90f) {
					obj.GetComponent<Controls> ().protation =0f;
				}
				else if (obj.GetComponent<Controls> ().protation == 180f) {
					obj.GetComponent<Controls> ().protation =90f;
				}
				else if (obj.GetComponent<Controls> ().protation == 270f) {
					obj.GetComponent<Controls> ().protation =180f;
				}
				GetComponent<SnapToGrid> ().colside = "right";

				Camera.main.GetComponent<CameraFollow> ().unpausegame ();
				characterPause.UnPauseCharacter ();
				UnFreezePlayer (obj);
			}
		}

	}

	public void FreezePlayer(GameObject obj)
	{
		obj.GetComponent<Character> ().Freeze ();
	}

	public void UnFreezePlayer(GameObject obj)
	{
		obj.transform.GetChild (2).transform.localEulerAngles = Vector3.zero;
		obj.transform.GetChild (2).transform.localPosition = Vector3.zero;
		obj.GetComponent<Character> ().UnFreeze ();
	}
}
