using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acivemapname : MonoBehaviour {
	public string activem="";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main != null) {
			if (Camera.main.GetComponent<networkcamera> () != null) {
				if (Camera.main.GetComponent<networkcamera> ().activem != "") {
					activem = Camera.main.GetComponent<networkcamera> ().activem;
				}
			}
			if (Camera.main.GetComponent<CameraFollow> () != null) {
				if (Camera.main.GetComponent<CameraFollow> ().activem!="") {
					activem = Camera.main.GetComponent<CameraFollow> ().activem;
				}
			}
		}
	}
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
