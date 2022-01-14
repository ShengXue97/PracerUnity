using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {
	public GameObject SelectedBlock;
	// Use this for initialization
	void Start () {
		if (GetComponent<PhotonView> ().isMine) {
			
		} else {
			//GetComponent<SpriteRenderer> ().color = new Color(142f,255f,174f,1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main == null) {
			return;
		}
		if (Camera.main.GetComponent<Transform> ().rotation != Quaternion.Euler (new Vector3 (0, 0, 0))) {
			GetComponent<Transform> ().rotation = Camera.main.GetComponent<Transform> ().rotation;
		}
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, 0.01f,Camera.main.GetComponent<CameraFollow>().whatIsUI);

		if (hitColliders.Length == 0) {
			Camera.main.GetComponent<CameraFollow> ().canedit = true;
		} else {
			Camera.main.GetComponent<CameraFollow> ().canedit = false;
		}

		//SelectedBlock follows cursor
		if (SelectedBlock != null) {
			SelectedBlock.transform.position = transform.position;
		}
		//Track release of cursor using custom cursor's sprite
		if (GetComponent<SpriteRenderer> ().sprite == Camera.main.GetComponent<CameraFollow> ().CursorSelectedReleased) {
			//If this cursor belongs to me
			string username=name.Split(';')[0];
			if (Camera.main.GetComponent<CameraFollow> ().LocalPhotonView.owner.NickName == username) {
				Camera.main.GetComponent<CameraFollow> ().LocalPhotonView.RPC ("SendCursorState", PhotonTargets.AllBuffered, 0, username, "",transform.position);


			}
		}
	}

	public void SetSelectedBlock(GameObject block)
	{
		SelectedBlock = block;
	}
}
