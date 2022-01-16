using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBlock : MonoBehaviour {
	bool canenable=true;
	bool visible=true;
	// Use this for initialization
	void Start () {
		//StartCoroutine (checkvisible (Random.Range(0.01f,1f)));
	}
	
	// Update is called once per frame
	void Update () {/*
		Collider2D[] playercolliders = Physics2D.OverlapCircleAll (transform.position, 1f,Camera.main.GetComponent<CameraFollow>().whatIsBlockCheck);

		if (playercolliders.Length == 0) {
			visible = false;
		} else {
			visible = true;
		}
		*/
		return;
		if (!GetComponent<Renderer> ().isVisible) {
			return;
		}
		else {
			visible = true;
		}

		//Disable components of block when not visible
		if (!visible)
		{
			if (GetComponent<SnapToGrid> () != null) {
				GetComponent<SnapToGrid> ().enabled = false;
			}
			if (GetComponent<Rigidbody2D> () != null) {
				GetComponent<Rigidbody2D> ().Sleep ();
			}
			if (GetComponent<BoxCollider2D> () != null) {
				GetComponent<BoxCollider2D> ().enabled = false;
			}
			canenable = true;
		}
		else if (visible && canenable)
		{
			canenable = false;
			if (GetComponent<SnapToGrid> () != null) {
				GetComponent<SnapToGrid> ().enabled = true;
			}
			if (GetComponent<Rigidbody2D> () != null) {
				GetComponent<Rigidbody2D> ().WakeUp ();
			}
			if (GetComponent<BoxCollider2D> () != null) {
				GetComponent<BoxCollider2D> ().enabled = true;
			}
		}
	}

}
