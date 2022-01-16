using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {
	public CameraFollow camfollow;
	public GameObject blockparent;
	// Use this for initialization
	void Start () {
		camfollow = Camera.main.GetComponent<CameraFollow> ();
		var split=name.Split(';');
		blockparent.name = "blockparent" + split [1] + ";" + split [2];
	}
	
	// Update is called once per frame
	void Update () {
		//player chunk location
		float Pchunkx=(camfollow.playerx / 100)-0.5f;
		float Pchunky=(camfollow.playery / 100)-0.5f;

		//this object chunk location
		var split=name.Split(';');
		float Schunkx = float.Parse(split [1]);
		float Schunky = float.Parse(split [2]);

		//Enable chunk only if player is nearby
		float maxfloat=0.2f;

		if (blockparent.activeSelf == false) {
			if (Mathf.Abs (Schunkx - Pchunkx) < maxfloat && Mathf.Abs (Schunky - Pchunky) < maxfloat) {
				blockparent.SetActive (true);
			}
			else if (Mathf.Abs (Schunkx - Pchunkx) < maxfloat && Mathf.Abs (Schunky - Pchunky) >= maxfloat) {
				blockparent.SetActive (true);
			}
			else if (Mathf.Abs (Schunkx - Pchunkx) >= maxfloat && Mathf.Abs (Schunky - Pchunky) < maxfloat) {
				blockparent.SetActive (true);
			}
		} else {
			if (Mathf.Abs (Schunkx - Pchunkx) >= maxfloat && Mathf.Abs (Schunky - Pchunky) >= maxfloat) {
				blockparent.SetActive (false);
			}
		}
	}
}
