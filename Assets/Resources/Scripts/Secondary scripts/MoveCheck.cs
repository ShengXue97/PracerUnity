using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheck : MonoBehaviour {
	public string direction;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag != "Player" && collider.gameObject.tag!="MoveCheck") {
			if (collider.gameObject.GetComponent<SnapToGrid> () != null) {
				if (collider.gameObject.GetComponent<SnapToGrid> ().direction == "") {
					if (direction == "horizontal") {
						collider.gameObject.GetComponent<SnapToGrid> ().AlwaysActiveHorizontal ();
					}
					else if (direction == "vertical") {
						collider.gameObject.GetComponent<SnapToGrid> ().AlwaysActiveVertical ();
					}
					Destroy (gameObject);
				}
			}
		}
	}
}
