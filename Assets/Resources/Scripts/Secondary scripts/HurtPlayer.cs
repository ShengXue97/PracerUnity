using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour {
	public string owner="";
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject other = collider.gameObject;

		if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			//Ignore collisions during editing in level editor
			return;
		}

		if (other.tag == "Player") {
			//Special collision with player
			PlayerCollide (other);
			return;
		}
		if (other.tag == "Projectiles" || other.tag == "RocketProjectiles" || other.tag=="teleportcheck") {
			//Ignore collisions with other projectiles,and teleportcheck of players.
			return;
		}
		if (tag != "brick" && tag != "bomb" && tag != "crumble") {
			if (other.tag == "brick" || other.tag == "bomb" || other.tag == "crumble") {
				if (Camera.main.GetComponent<CameraFollow> ().editplay == 0) {
					if (other.GetComponent<SnapToGrid> () != null) {
						Camera.main.GetComponent<CameraFollow> ().mapd.Remove (other.GetComponent<SnapToGrid> ().gridposString);
						Camera.main.GetComponent<CameraFollow> ().blockmapd.Remove (other.GetComponent<SnapToGrid> ().gridposString);
						Camera.main.GetComponent<CameraFollow> ().LocalPhotonView.RPC ("ServerRemoveBlock", PhotonTargets.Others, other.GetComponent<SnapToGrid> ().gridposString);
						Destroy (other);
					}
				} else {
					Camera.main.GetComponent<CameraFollow> ().disabledblocks.Add (other);
					other.SetActive (false);
				}

				if (tag == "RocketProjectiles" || tag == "MeleeBox") {
					//Spawn explosion for rockets
					Instantiate (Camera.main.GetComponent<CameraFollow> ().RocketExplosion, transform.position, Quaternion.identity);
				}
			}

			else if (other.tag == "vanish") {
				if (other.GetComponent<SnapToGrid>().fading == "none" || other.GetComponent<SnapToGrid>().fading == "fadingin")
				{
					other.GetComponent<SnapToGrid>().startTime = Time.time;
					other.GetComponent<SnapToGrid>().fading = "fadingout";

				}
				if (tag == "RocketProjectiles" || tag == "MeleeBox") {
					//Spawn explosion for rockets
					Instantiate (Camera.main.GetComponent<CameraFollow> ().RocketExplosion, transform.position, Quaternion.identity);
				}
			}
		}

		if (tag != "RocketProjectiles" && tag != "DamageArea" && tag != "MeleeBox") {
			if (tag != "brick" && tag != "bomb" && tag != "crumble") {
				if (other.tag != "portal") {
					gameObject.SetActive (false);
					Instantiate (Camera.main.GetComponent<CameraFollow> ().RocketExplosion, transform.position, Quaternion.identity);
				}
			}
		}

		if (tag == "RocketProjectiles") {
			//Destroy rocket is not collided with destructible blocks
			if (other.tag != "brick" && other.tag != "bomb" && other.tag != "crumble" && other.tag!="portal" && other.tag!="vanish") {
				Instantiate (Camera.main.GetComponent<CameraFollow> ().RocketExplosion, transform.position, Quaternion.identity);
				gameObject.SetActive (false);
			}
		}

	}

	public void PlayerCollide(GameObject other)
	{
		if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("LevelEditor")) {
			//Ignore collisions during editing in level editor
			return;
		}
		//Only allow damage from top of spikes
		if (tag == "spike") {
			if (GetComponent<SnapToGrid> ().colside != "top") {
				return;
			}
		}
		//Ignore with the owner of the object
		if (owner != "") {
			if (owner == other.GetComponent<Controls> ().user) {
				return;
			}
		}
		Controls pco = other.GetComponent<Controls> ();
		Rigidbody2D prb = other.GetComponent<Rigidbody2D> ();

		if (Camera.main.GetComponent<CameraFollow>().GameMode == "Deathmatch" && other.GetComponent<PhotonView> () != null) {
			other.gameObject.GetComponent<PhotonView> ().photonView.RPC ("ServerTakeDamage", PhotonTargets.All, other.gameObject.name, 10);
		}

		other.GetComponent<CharacterDash> ().StunPlayer ();
		Controllers controller = GameObject.FindGameObjectWithTag ("controller").GetComponent<Controllers> ();
		controller.PlayOnce (controller.a_explosion);

		AddExplosionForce(pco.gameObject, 5f, transform.position, 20f);
		Instantiate (Camera.main.GetComponent<CameraFollow> ().CrateExplode,transform.position, Quaternion.identity);

		if (tag != "spike") {
			if (Camera.main.GetComponent<CameraFollow> ().editplay == 0 && SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Level1")) {
				if (other.tag != "rocket" && other.tag != "portal") {
					if (GetComponent<SnapToGrid> () != null) {
						Camera.main.GetComponent<CameraFollow> ().mapd.Remove (GetComponent<SnapToGrid> ().gridposString);
						Camera.main.GetComponent<CameraFollow> ().blockmapd.Remove (GetComponent<SnapToGrid> ().gridposString);
					}
					Camera.main.GetComponent<CameraFollow>().LocalPhotonView.RPC ("ServerRemoveBlock", PhotonTargets.Others, GetComponent<SnapToGrid> ().gridposString);

					Destroy (gameObject);

				}
			} else {
				Camera.main.GetComponent<CameraFollow> ().disabledblocks.Add (gameObject);
				if (other.tag != "rocket" && other.tag != "portal") {
					gameObject.SetActive (false);
				}
			}
		}
	}
	public static void AddExplosionForce(GameObject body, float expForce, Vector3 expPosition, float expRadius)
	{

		var dir = (body.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0)
		{
			calc = 0;
		}
		CorgiController corgicontroller = body.GetComponent<CorgiController> ();
		corgicontroller.AddForce (dir.normalized * expForce * calc);
	}


}
